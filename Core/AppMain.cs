using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using DNHper;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;

namespace DaemonKit.Core {
    class AppMain {
        static RegistryKey runKey = Registry.CurrentUser.OpenSubKey (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        public static string ConfigPath {
            get {
                return Path.Combine (Path.GetDirectoryName (Process.GetCurrentProcess ().MainModule.FileName), "config.xml");
            }
        }
        public static AppConfig appConfig;
        private static string mainProcess = string.Empty;
        private static float delayTime = 5.0f, intervalTime = 10.0f;
        private static bool bKeepTop = true;
        private static bool bAutoStart = true;
        private static bool bRunAs = true;
        public static void syncConfig () {
            mainProcess = appConfig.MainProcess;
            delayTime = appConfig.DelayTime;
            intervalTime = appConfig.IntervalTime;
            bKeepTop = appConfig.KeepTop;
            bAutoStart = appConfig.AutoStart;
            bRunAs = appConfig.RunAs;
            var _executorPath = Process.GetCurrentProcess ().MainModule.FileName;
            var _uniKey = "DaemonKit_" + _executorPath.ToMD5 ();

            if (bAutoStart) {
                runKey.SetValue (_uniKey, _executorPath);
                if (!TaskService.Instance.AllTasks.ToList ().Exists (_task => _task.Name == _uniKey)) {
                    TaskDefinition td = TaskService.Instance.NewTask ();
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Actions.Add (_executorPath, appConfig.Arguments);

                    LogonTrigger lt = new LogonTrigger ();
                    td.Triggers.Add (lt);
                    td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition (_uniKey, td);
                }
            } else {
                runKey.DeleteValue (_uniKey, false);
                if (TaskService.Instance.AllTasks.ToList ().Exists (_task => _task.Name == _uniKey)) {
                    TaskService.Instance.RootFolder.DeleteTask (_uniKey, false);
                }
            }
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
                    ProcManager.DaemonProcess (mainProcess, appConfig.Arguments, bRunAs);
                    if (bKeepTop) {
                        ProcManager.KeepTopWindow (Path.GetFileNameWithoutExtension (mainProcess));
                    }
                });
        }
    }
}