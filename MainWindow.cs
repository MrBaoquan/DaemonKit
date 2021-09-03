using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaemonKit.Core;
using DNHper;
using Hardware.Info;

namespace DaemonKit {
    public partial class DaemonKit : Form {
        static readonly HardwareInfo hardwareInfo = new HardwareInfo ();
        public static string ExtensionConfigPath {
            get {
                return Path.Combine (Path.GetDirectoryName (Process.GetCurrentProcess ().MainModule.FileName), "extension.xml");
            }
        }
        public static string ExtensionPath {
            get {
                return Path.Combine (Path.GetDirectoryName (Process.GetCurrentProcess ().MainModule.FileName), "Extensions");
            }
        }
        ExtentionConfig extensionConfig = null;
        public DaemonKit () {

            InitializeComponent ();
            #region 菜单栏设置
            ts_options.ShowShortcutKeys = true;
            ts_options.ShortcutKeys = Keys.Alt | Keys.O;
            ts_options.DropDown.Closing += (sender, e) => {
                var _args = e as ToolStripDropDownClosingEventArgs;
                if (_args.CloseReason == ToolStripDropDownCloseReason.ItemClicked) {
                    _args.Cancel = true;
                } else {
                    Observable.Start (() => {
                            AppMain.syncConfig ();
                        }).ObserveOn (this)
                        .Subscribe ();
                }
            };

            menu_autoStart.CheckOnClick = true;
            menu_autoStart.ShowShortcutKeys = true;
            menu_autoStart.ShortcutKeys = Keys.Alt | Keys.D1;
            menu_autoStart.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.AutoStart = menu_autoStart.Checked;
                syncConfig2UI ();
                AppMain.SaveConfig ();
            };

            menu_keepTop.CheckOnClick = true;
            menu_keepTop.ShowShortcutKeys = true;
            menu_keepTop.ShortcutKeys = Keys.Alt | Keys.D2;
            menu_keepTop.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.KeepTop = menu_keepTop.Checked;
                syncConfig2UI ();
                AppMain.SaveConfig ();
            };

            menu_RunAs.CheckOnClick = true;
            menu_RunAs.ShowShortcutKeys = true;
            menu_RunAs.ShortcutKeys = Keys.Alt | Keys.D3;
            menu_RunAs.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.RunAs = menu_RunAs.Checked;
                syncConfig2UI ();
                AppMain.SaveConfig ();
            };

            menu_globalShortcut.CheckOnClick = true;
            menu_globalShortcut.ShowShortcutKeys = true;
            menu_globalShortcut.ShortcutKeys = Keys.Alt | Keys.D4;
            menu_globalShortcut.CheckedChanged += (sendor, e) => {

                ts_options.ShowDropDown ();
                AppMain.appConfig.GlobalShortcut = menu_globalShortcut.Checked;
                syncConfig2UI ();
                AppMain.SaveConfig ();
                if (menu_globalShortcut.Checked) {
                    RegisterHotKey ();
                } else {
                    UnregisterHotKey ();
                }
            };

            /// <summary>
            /// 打开、杀死进程
            /// </summary>
            menu_openProcess.ShortcutKeys = Keys.Control | Keys.R;
            menu_openProcess.ShowShortcutKeys = true;
            menu_openProcess.Click += (sendor, e) => {
                if (btn_confirm.Text == "开始守护") {
                    startDaemon ();
                }
                AppMain.OpenProcess ();
            };
            menu_killProcess.ShortcutKeys = Keys.Control | Keys.W;
            menu_killProcess.ShowShortcutKeys = true;
            menu_killProcess.Click += (sendor, e) => {
                AppMain.KillProcess ();
                if (btn_confirm.Text == "暂停守护") {
                    stopDaemon ();
                }
            };

            menu_selectProcess.ShortcutKeys = Keys.Control | Keys.O;
            menu_selectProcess.ShowShortcutKeys = true;
            menu_selectProcess.Click += (sender, e) => {
                openFileDialog.ShowDialog ();
            };

            menu_backup.ShortcutKeys = Keys.Control | Keys.B;
            menu_backup.ShowShortcutKeys = true;
            IDisposable _copyHandler = null;
            menu_backup.Click += (sender, e) => {
                if (_copyHandler != null) {
                    _copyHandler.Dispose ();
                    _copyHandler = null;
                }

                //TODO D盘不存在时  让用户自主选择备份目录
                if (!Directory.Exists (Path.GetPathRoot (AppMain.appConfig.BackupDir))) {
                    AppMain.appConfig.BackupDir = AppMain.appConfig.BackupDir.Replace (Path.GetPathRoot (AppMain.appConfig.BackupDir), "C:\\");
                }
                _copyHandler = IOManger.CopyDir (
                        Path.GetDirectoryName (AppMain.MainProcess),
                        Path.Combine (AppMain.appConfig.BackupDir, Path.GetFileNameWithoutExtension (AppMain.MainProcess))
                    )
                    .SubscribeOn (ThreadPoolScheduler.Instance)
                    .ObserveOn (this)
                    .Subscribe (_ => {
                        NLogger.Debug ("[DK]: 备份中:{0}, ({1})", _.filename, (_.copied * 100 / (float) _.total).ToString ("0.00") + "%");
                    }, _err => {
                        NLogger.Error ("[DK]: 文件备份错误:{0}", _err);
                    }, () => {
                        NLogger.Info ("[DK]: 文件备份完成...");
                    });
            };

            menu_processDir.ShortcutKeys = Keys.Control | Keys.D1;
            menu_processDir.ShowShortcutKeys = true;
            menu_processDir.Click += (sender, e) => {
                WinAPI.OpenProcess ("explorer.exe", Path.GetDirectoryName (AppMain.MainProcess));
            };

            menu_userProfile.ShortcutKeys = Keys.Control | Keys.D2;
            menu_userProfile.ShowShortcutKeys = true;
            menu_userProfile.Click += (sender, e) => {
                WinAPI.OpenProcess ("explorer.exe", Environment.GetFolderPath (Environment.SpecialFolder.UserProfile));
            };

            menu_startup.ShortcutKeys = Keys.Control | Keys.D3;
            menu_startup.ShowShortcutKeys = true;
            menu_startup.Click += (sender, e) => {
                WinAPI.OpenProcess ("explorer.exe", Environment.GetFolderPath (Environment.SpecialFolder.Startup));
            };

            menu_configDir.ShortcutKeys = Keys.Control | Keys.E;
            menu_configDir.ShowShortcutKeys = true;
            menu_configDir.Click += (sender, e) => {
                WinAPI.OpenProcess ("notepad.exe", AppMain.ConfigPath);
            };

            menu_cmd.ShortcutKeys = Keys.Control | Keys.T;
            menu_cmd.ShowShortcutKeys = true;
            menu_cmd.Click += (sendor, e) => {
                WinAPI.OpenProcess ("cmd.exe", "", true);
            };

            menu_powershell.ShortcutKeys = Keys.Control | Keys.P;
            menu_powershell.ShowShortcutKeys = true;
            menu_powershell.Click += (sendor, e) => {
                WinAPI.OpenProcess ("powershell.exe", "", true);
            };

            menu_about.Click += (sender, e) => {
                new About ().ShowDialog ();
            };

            #endregion
            #region 拓展菜单栏

            if (!File.Exists (ExtensionConfigPath)) {
                USerialization.SerializeXML (new ExtentionConfig (), ExtensionConfigPath);
            };

            try {
                extensionConfig = USerialization.DeserializeXML<ExtentionConfig> (ExtensionConfigPath);
                var _toolStrip = new ToolStripMenuItem (extensionConfig.Name);

                extensionConfig.Extentions.WithIndex ().ToList ().ForEach (_extention => {
                    var _item = new ToolStripMenuItem (_extention.item.Name);
                    _item.Click += (sendor, e) => {
                        var _extensionPath = Path.Combine (ExtensionPath, _extention.item.Path);
                        if (!Path.IsPathRooted (_extention.item.Path) && File.Exists (_extensionPath)) {
                            WinAPI.OpenProcess (_extensionPath, _extention.item.Args, _extention.item.RunAs);
                        } else {
                            WinAPI.OpenProcess (_extention.item.Path, _extention.item.Args, _extention.item.RunAs);
                        }
                    };
                    _item.ShowShortcutKeys = true;
                    _item.ShortcutKeys = Keys.Control | (Keys) (Keys.F1 + _extention.index);
                    _toolStrip.DropDownItems.Add (_item);
                });
                mainMenuStrip.Items.Insert (3, _toolStrip);
            } catch (System.Exception) { }
            #endregion
        }

        private void syncFormStatus () {
            if (btn_confirm.Text != "开始守护") {
                textbox_MainProcess.Enabled = false;
                textbox_intervalTime.Enabled = false;
                textbox_delayTime.Enabled = false;
            } else {
                textbox_MainProcess.Enabled = true;
                textbox_intervalTime.Enabled = true;
                textbox_delayTime.Enabled = true;
            }
        }

        private void syncConfig2UI () {
            textbox_MainProcess.Text = Path.GetFileName (AppMain.appConfig.MainProcess);
            textbox_intervalTime.Text = AppMain.appConfig.IntervalTime.ToString ("0.0");
            textbox_delayTime.Text = AppMain.appConfig.DelayTime.ToString ("0.0");

            menu_autoStart.Checked = AppMain.appConfig.AutoStart;
            menu_keepTop.Checked = AppMain.appConfig.KeepTop;
            menu_RunAs.Checked = AppMain.appConfig.RunAs;
            menu_globalShortcut.Checked = AppMain.appConfig.GlobalShortcut;
        }

        public void Log (string InMsg) {
            if (text_logbox.Text == InMsg) return;
            text_logbox.Text = InMsg;
            text_logbox.SelectionStart = InMsg.Length;
            text_logbox.ScrollToCaret ();
            text_logbox.Refresh ();
        }

        OpenFileDialog openFileDialog = new OpenFileDialog ();
        private void DaemonKit_Load (object sender, EventArgs e) {
            this.MaximizeBox = false;
            // this.MinimizeBox = false;
            this.ActiveControl = null;

            AppMain.ProgramEntry (this);
            syncConfig2UI ();
            syncFormStatus ();

            openFileDialog.InitialDirectory = Path.GetDirectoryName (AppMain.appConfig.MainProcess);
            openFileDialog.Filter = "可执行文件(*.exe)|*.exe";
            openFileDialog.FileOk += (o, args) => {
                AppMain.appConfig.MainProcess = openFileDialog.FileName;
                openFileDialog.InitialDirectory = Path.GetDirectoryName (AppMain.appConfig.MainProcess);
                syncConfig2UI ();
                syncFormStatus ();
            };

            if (!File.Exists (AppMain.MainProcess)) {
                NLogger.Info ("[DK]: 守护进程路径不存在, 请选择正确的进程路径");
                openFileDialog.InitialDirectory = Path.GetPathRoot (AppMain.MainProcess);
                openFileDialog.ShowDialog ();
                AppMain.ClearDeamon ();
                btn_confirm.Text = "开始守护";
            } else {
                btn_confirm.Text = "暂停守护";
            }

            FetchHardwareInfo (null, null);
        }

        private void OnSelectMainProcess (object sender, EventArgs e) {
            openFileDialog.ShowDialog ();
        }

        private void stopDaemon () {
            btn_confirm.Text = "开始守护";
            AppMain.ClearDeamon ();
            NLogger.Info ("[DK]: 暂停守护任务");

            syncFormStatus ();
        }

        private void startDaemon () {

            btn_confirm.Text = "执行中...";
            btn_confirm.Enabled = false;

            AppMain.SaveConfig ();
            AppMain.syncConfig ();
            AppMain.RunDaemonTask ();
            btn_confirm.Text = "暂停守护";
            btn_confirm.Enabled = true;

            syncFormStatus ();
        }

        private void OnApply (object sender, EventArgs e) {
            if (btn_confirm.Text == "暂停守护") {
                stopDaemon ();
            } else {
                startDaemon ();
            }
        }

        private void OnDelayInput (object sender, EventArgs e) {
            AppMain.appConfig.DelayTime = textbox_delayTime.Text.Parse2Float (0.1f);
        }

        private void OnIntervalInput (object sender, EventArgs e) {
            AppMain.appConfig.IntervalTime = textbox_intervalTime.Text.Parse2Float (0.1f);
        }

        protected override void WndProc (ref Message m) {
            const int WM_HOTKEY = 0X0312;
            switch (m.Msg) {
                case WM_HOTKEY:
                    if (!AppMain.appConfig.GlobalShortcut) {
                        break;
                    }
                    if (m.WParam.ToInt32 () == 100) {
                        WinAPI.SetWindowPos (this.Handle, (int) HWndInsertAfter.HWND_TOPMOST,
                            0, 0, 0, 0,
                            SetWindowPosFlags.SWP_SHOWWINDOW | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_FRAMECHANGED);
                        WinAPI.ShowWindow (this.Handle, (int) CMDShow.SW_SHOWNORMAL);
                    } else if (m.WParam.ToInt32 () == 101) {
                        if (btn_confirm.Text == "开始守护") {
                            startDaemon ();
                        }
                        AppMain.OpenProcess ();
                    } else if (m.WParam.ToInt32 () == 102) {
                        AppMain.KillProcess ();
                        if (btn_confirm.Text == "暂停守护") {
                            stopDaemon ();
                        }
                    }
                    break;
            }
            base.WndProc (ref m);
        }

        private void DaemonKit_Unload (object sender, FormClosingEventArgs e) {
            WinAPI.UnregisterHotKey (this.Handle, 100);
            WinAPI.UnregisterHotKey (this.Handle, 101);
            WinAPI.UnregisterHotKey (this.Handle, 102);
        }

        public void RegisterHotKey () {
            // 注册全局热键
            // 置顶守护窗口
            WinAPI.RegisterHotKey (this.Handle, 100, (uint) KeyModifiers.Ctrl, (uint) Keys.D);
            // 运行进程
            WinAPI.RegisterHotKey (this.Handle, 101, (uint) (KeyModifiers.Ctrl), (uint) Keys.R);
            // 杀死进程
            WinAPI.RegisterHotKey (this.Handle, 102, (uint) (KeyModifiers.Ctrl), (uint) Keys.W);
        }

        public void UnregisterHotKey () {
            WinAPI.UnregisterHotKey (this.Handle, 100);
            WinAPI.UnregisterHotKey (this.Handle, 101);
            WinAPI.UnregisterHotKey (this.Handle, 102);
        }

        private void FetchHardwareInfo (object sender, MouseEventArgs e) {
            text_information.Text = "硬件信息玩命读取中...";

            Observable.Start<string> (() => {
                hardwareInfo.RefreshCPUList ();
                hardwareInfo.RefreshVideoControllerList ();
                hardwareInfo.RefreshMemoryList ();
                hardwareInfo.RefreshNetworkAdapterList ();
                hardwareInfo.RefreshMonitorList ();
                hardwareInfo.RefreshBIOSList ();
                hardwareInfo.RefreshMotherboardList ();
                var _description = HardwareInfo.GetLocalIPv4Addresses ().Aggregate ("IPv4地址:" + "\r\n", (_current, _next) => { return _current + _next + "\r\n"; });
                _description = hardwareInfo.CpuList.Aggregate (_description + "\r\nCPU:\r\n", (_current, _next) => { return _current + _next.Name; });
                _description = hardwareInfo.VideoControllerList.Aggregate (_description + "\r\n\r\nGPU:\r\n", (_current, _next) => { return _current + _next.Name; });
                _description = hardwareInfo.MemoryList.Aggregate (_description + "\r\n\r\n内存:\r\n", (_current, _next) => {
                    return _current +
                        string.Format ("{0}-{1}({2})", _next.Manufacturer, _next.PartNumber, _next.Capacity.FormatBytes ()) + "\r\n";
                });
                _description = hardwareInfo.MonitorList.Aggregate (_description + "\r\n显示器:\r\n", (_current, _next) => { return _current + _next.Name + "\r\n"; });
                _description = hardwareInfo.BiosList.Aggregate (_description + "\r\nBIOS:\r\n", (_current, _next) => { return _current + _next.Manufacturer + " " + _next.Version + "\r\n"; });
                _description = hardwareInfo.MotherboardList.Aggregate (_description + "\r\n主板:\r\n", (_current, _next) => { return _current + _next.Manufacturer + " " + _next.Product + "\r\n"; });
                return _description;
            }).ObserveOn (this).Subscribe (_description => {
                text_information.ScrollBars = ScrollBars.Vertical;
                text_information.Text = _description;
            });

        }
    }
}