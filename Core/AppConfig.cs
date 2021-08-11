using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonKit.Core {
    public class AppConfig {
        public string MainProcess = @"C:\Windows\System32\notepad.exe";
        public bool KeepTop = true;
        public float DelayTime = 15.0f;
        public float IntervalTime = 30.0f;
    }
}