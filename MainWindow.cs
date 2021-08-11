using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaemonKit.Core;
using DNHper;
using Newtonsoft.Json;

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
                Console.WriteLine (checkbox_KeepTop.Checked);
                AppMain.appConfig.KeepTop = checkbox_KeepTop.Checked;
                syncConfig2UI ();
            };
        }

        private void OnSelectMainProcess (object sender, EventArgs e) {
            openFileDialog.ShowDialog ();
            Console.WriteLine ("clicked.");
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