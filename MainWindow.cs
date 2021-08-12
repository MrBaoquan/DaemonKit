using System;
using System.IO;
using System.Windows.Forms;
using DaemonKit.Core;
using DNHper;

namespace DaemonKit {
    public partial class DaemonKit : Form {

        public DaemonKit () {
            InitializeComponent ();
        }

        private void syncConfig2UI () {
            textbox_MainProcess.Text = Path.GetFileName (AppMain.appConfig.MainProcess);
            textbox_intervalTime.Text = AppMain.appConfig.IntervalTime.ToString ("0.0");
            textbox_delayTime.Text = AppMain.appConfig.DelayTime.ToString ("0.0");
            checkbox_KeepTop.Checked = AppMain.appConfig.KeepTop;
            checkbox_autoStart.Checked = AppMain.appConfig.AutoStart;
            checkbox_RunAs.Checked = AppMain.appConfig.RunAs;
        }

        OpenFileDialog openFileDialog = new OpenFileDialog ();
        private void DaemonKit_Load (object sender, EventArgs e) {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            AppMain.ProgramEntry ();
            syncConfig2UI ();

            openFileDialog.InitialDirectory = Path.GetDirectoryName (AppMain.appConfig.MainProcess);
            openFileDialog.Filter = "可执行文件(*.exe)|*.exe";
            openFileDialog.FileOk += (o, args) => {
                AppMain.appConfig.MainProcess = openFileDialog.FileName;
                syncConfig2UI ();
            };

            checkbox_KeepTop.CheckedChanged += (o, args) => {
                AppMain.appConfig.KeepTop = checkbox_KeepTop.Checked;
            };
            checkbox_autoStart.CheckedChanged += (o, args) => {
                AppMain.appConfig.AutoStart = checkbox_autoStart.Checked;
            };

            checkbox_RunAs.CheckedChanged += (o, args) => {
                AppMain.appConfig.RunAs = checkbox_RunAs.Checked;
            };
        }

        private void OnSelectMainProcess (object sender, EventArgs e) {
            openFileDialog.ShowDialog ();
        }

        private void OnApply (object sender, EventArgs e) {
            AppMain.SaveConfig ();
            AppMain.syncConfig ();
            AppMain.Daemon ();
        }

        private void OnDelayInput (object sender, EventArgs e) {
            AppMain.appConfig.DelayTime = textbox_delayTime.Text.Parse2Float ();
        }

        private void OnIntervalInput (object sender, EventArgs e) {
            AppMain.appConfig.IntervalTime = textbox_intervalTime.Text.Parse2Float ();
        }
    }
}