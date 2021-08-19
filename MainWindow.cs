using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public DaemonKit () {

            InitializeComponent ();

            ts_options.ShowShortcutKeys = true;
            ts_options.ShortcutKeys = Keys.Alt | Keys.O;
            ts_options.DropDown.Closing += (sender, e) => {
                var _args = e as ToolStripDropDownClosingEventArgs;
                if (_args.CloseReason == ToolStripDropDownCloseReason.ItemClicked) {
                    _args.Cancel = true;
                }
            };

            menu_autoStart.CheckOnClick = true;
            menu_autoStart.ShowShortcutKeys = true;
            menu_autoStart.ShortcutKeys = Keys.Alt | Keys.D1;
            menu_autoStart.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.AutoStart = menu_autoStart.Checked;
                syncConfig2UI ();
            };

            menu_keepTop.CheckOnClick = true;
            menu_keepTop.ShowShortcutKeys = true;
            menu_keepTop.ShortcutKeys = Keys.Alt | Keys.D2;
            menu_keepTop.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.KeepTop = menu_keepTop.Checked;
                syncConfig2UI ();
            };

            menu_RunAs.CheckOnClick = true;
            menu_RunAs.ShowShortcutKeys = true;
            menu_RunAs.ShortcutKeys = Keys.Alt | Keys.D3;
            menu_RunAs.CheckedChanged += (sender, e) => {
                ts_options.ShowDropDown ();
                AppMain.appConfig.RunAs = menu_RunAs.Checked;
                syncConfig2UI ();
            };

            menu_selectProcess.ShortcutKeys = Keys.Control | Keys.O;
            menu_selectProcess.ShowShortcutKeys = true;
            menu_selectProcess.Click += (sender, e) => {
                openFileDialog.ShowDialog ();
            };

            menu_processDir.ShortcutKeys = Keys.Control | Keys.D1;
            menu_processDir.ShowShortcutKeys = true;
            menu_processDir.Click += (sender, e) => {
                WinAPI.OpenProcess ("explorer.exe", Path.GetDirectoryName (AppMain.MainProcess));
            };

            menu_backup.ShortcutKeys = Keys.Control | Keys.D2;
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

            menu_configDir.ShortcutKeys = Keys.Control | Keys.E;
            menu_configDir.ShowShortcutKeys = true;
            menu_configDir.Click += (sender, e) => {
                WinAPI.OpenProcess ("notepad.exe", AppMain.ConfigPath);
            };

            menu_about.Click += (sender, e) => {
                new About ().ShowDialog ();
            };
        }

        private void syncConfig2UI () {
            textbox_MainProcess.Text = Path.GetFileName (AppMain.appConfig.MainProcess);
            textbox_intervalTime.Text = AppMain.appConfig.IntervalTime.ToString ("0.0");
            textbox_delayTime.Text = AppMain.appConfig.DelayTime.ToString ("0.0");

            menu_autoStart.Checked = AppMain.appConfig.AutoStart;
            menu_keepTop.Checked = AppMain.appConfig.KeepTop;
            menu_RunAs.Checked = AppMain.appConfig.RunAs;
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

            // 注册全局热键
            WinAPI.RegisterHotKey (this.Handle, 100, (uint) KeyModifiers.Ctrl, (uint) Keys.D);

            openFileDialog.InitialDirectory = Path.GetDirectoryName (AppMain.appConfig.MainProcess);
            openFileDialog.Filter = "可执行文件(*.exe)|*.exe";
            openFileDialog.FileOk += (o, args) => {
                AppMain.appConfig.MainProcess = openFileDialog.FileName;
                openFileDialog.InitialDirectory = Path.GetDirectoryName (AppMain.appConfig.MainProcess);
                syncConfig2UI ();
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

            text_information.Text = "玩命加载硬件信息中...";
            Observable.Start<string> (() => {
                hardwareInfo.RefreshCPUList ();
                hardwareInfo.RefreshVideoControllerList ();
                hardwareInfo.RefreshMemoryList ();
                hardwareInfo.RefreshNetworkAdapterList ();
                hardwareInfo.RefreshMonitorList ();
                hardwareInfo.RefreshBIOSList ();
                hardwareInfo.RefreshMotherboardList ();
                var _description = HardwareInfo.GetLocalIPv4Addresses ().Aggregate ("IP4地址:" + "\r\n", (_current, _next) => { return _current + _next + "\r\n"; });
                _description = hardwareInfo.CpuList.Aggregate (_description + "\r\nCPU:\r\n", (_current, _next) => { return _current + _next.Name; });
                _description = hardwareInfo.VideoControllerList.Aggregate (_description + "\r\n\r\nGPU:\r\n", (_current, _next) => { return _current + _next.Name; });
                _description = hardwareInfo.MemoryList.Aggregate (_description + "\r\n\r\n内存:\r\n", (_current, _next) => { return _current + _next.Manufacturer + _next.PartNumber + "\r\n"; });
                _description = hardwareInfo.MonitorList.Aggregate (_description + "\r\n显示器:\r\n", (_current, _next) => { return _current + _next.Name + "\r\n"; });
                _description = hardwareInfo.BiosList.Aggregate (_description + "\r\nBIOS:\r\n", (_current, _next) => { return _current + _next.Manufacturer + " " + _next.Version + "\r\n"; });
                _description = hardwareInfo.MotherboardList.Aggregate (_description + "\r\n主板:\r\n", (_current, _next) => { return _current + _next.Manufacturer + " " + _next.Product + "\r\n"; });
                return _description;
            }).ObserveOn (this).Subscribe (_description => {
                text_information.ScrollBars = ScrollBars.Vertical;
                text_information.Text = _description;
            });
        }

        private void OnSelectMainProcess (object sender, EventArgs e) {
            openFileDialog.ShowDialog ();
        }

        private void OnApply (object sender, EventArgs e) {
            if (btn_confirm.Text == "暂停守护") {
                btn_confirm.Text = "开始守护";
                AppMain.ClearDeamon ();
                NLogger.Info ("[DK]: 暂停守护任务");
            } else {
                btn_confirm.Text = "执行中...";
                btn_confirm.Enabled = false;

                AppMain.SaveConfig ();
                AppMain.syncConfig ();
                AppMain.Daemon ();
                btn_confirm.Text = "暂停守护";
                btn_confirm.Enabled = true;
            }
        }

        private void OnDelayInput (object sender, EventArgs e) {
            AppMain.appConfig.DelayTime = textbox_delayTime.Text.Parse2Float ();
        }

        private void OnIntervalInput (object sender, EventArgs e) {
            AppMain.appConfig.IntervalTime = textbox_intervalTime.Text.Parse2Float ();
        }

        protected override void WndProc (ref Message m) {
            const int WM_HOTKEY = 0X0312;
            switch (m.Msg) {
                case WM_HOTKEY:
                    if (m.WParam.ToInt32 () == 100) {
                        WinAPI.SetWindowPos (this.Handle, (int) HWndInsertAfter.HWND_TOPMOST,
                            0, 0, 0, 0,
                            SetWindowPosFlags.SWP_SHOWWINDOW | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_FRAMECHANGED);
                        WinAPI.ShowWindow (this.Handle, (int) CMDShow.SW_SHOWNORMAL);
                    }
                    break;
            }
            base.WndProc (ref m);
        }

        private void DaemonKit_Unload (object sender, FormClosingEventArgs e) {
            WinAPI.UnregisterHotKey (this.Handle, 100);
        }
    }
}