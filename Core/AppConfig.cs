using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonKit.Core {
    public class AppConfig {
        public string MainProcess = @"C:\Windows\System32\notepad.exe";
        public string BackupDir = @"D:\Daemonkit\Backups";
        public string Arguments = string.Empty;
        public bool KeepTop = true;
        public bool RunAs = true;
        public bool AutoStart = true;
        public bool GlobalShortcut = true;
        public float DelayTime = 15.0f;
        public float IntervalTime = 30.0f;
    }
}