using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DNHper;

namespace DaemonKit.Core {
    class AppMain {
        public static string ConfigPath {
            get {
                return Path.Combine (Path.GetDirectoryName (Process.GetCurrentProcess ().MainModule.FileName), "config.xml");
            }
        }
        public static AppConfig appConfig;

        private static string mainProcess = string.Empty;
        private static float delayTime = 5.0f, intervalTime = 10.0f;
        private static bool bKeepTop = true;

        public static void syncConfig () {
            mainProcess = appConfig.MainProcess;
            delayTime = appConfig.DelayTime;
            intervalTime = appConfig.IntervalTime;
            bKeepTop = appConfig.KeepTop;
        }

        public static void SaveConfig () {
            USerialization.SerializeXML (appConfig, ConfigPath);
        }

        public static void ProgramEntry () {
            NLogger.LogFileDir = "Logs";
            NLogger.Initialize ();
            if (!File.Exists (ConfigPath)) {
                USerialization.SerializeXML (new AppConfig (), ConfigPath);
            }
            appConfig = USerialization.DeserializeXML<AppConfig> (ConfigPath);
            syncConfig ();
            Daemon ();
        }

        private static IDisposable _checkHandler = null;
        public static void Daemon () {
            NLogger.Debug ("************************* Start DaemonkIT ****************************");
            if (_checkHandler != null) {
                _checkHandler.Dispose ();
                _checkHandler = null;
            }
            _checkHandler = Observable
                .Timer (TimeSpan.FromSeconds (delayTime), TimeSpan.FromSeconds (intervalTime))
                .Subscribe (_ => {
                    ProcManager.DaemonProcess (mainProcess);
                    if (bKeepTop) {
                        ProcManager.KeepTopWindow (Path.GetFileNameWithoutExtension (mainProcess));
                    }
                });
        }
    }
}