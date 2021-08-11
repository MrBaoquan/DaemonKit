using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNHper;

namespace DaemonKit.Core {
    class ProcManager {
        public static bool KeepTopWindow (string ProcessName) {
            var _process = WinAPI.FindProcess (ProcessName);
            if (_process == default (Process)) return false;
            //WinAPI.SetWindowLong(_process.MainWindowHandle, (int)SetWindowLongIndex.GWL_STYLE, (UInt32)GWL_STYLE.WS_POPUP);
            WinAPI.SetWindowPos (_process.MainWindowHandle, (int) HWndInsertAfter.HWND_TOPMOST,
                0, 0, 0, 0,
                SetWindowPosFlags.SWP_SHOWWINDOW | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_FRAMECHANGED);
            return true;
        }

        // 守护进程
        public static void DaemonProcess (string Path, string Args = "") {
            string _processName = System.IO.Path.GetFileNameWithoutExtension (Path);
            if (System.IO.Path.IsPathRooted (Path)) {
                // 如果进程未打开则打开该程序
                WinAPI.OpenProcessIfNotOpend (Path, Args);
            }

            var _process = WinAPI.FindProcess (_processName);
            if (_process == default (Process)) return;

            // 如果程序挂起 则关闭进程
            if (WinAPI.IsHungAppWindow (_process.MainWindowHandle)) {
                _process.Kill ();
                NLogger.Debug (string.Format ("process is hangup, killed it: {0}", _processName));
                return;
            }
        }
    }
}