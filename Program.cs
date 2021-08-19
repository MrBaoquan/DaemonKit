using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNHper;

namespace DaemonKit {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main () {
            var _currentProcessFileName = Process.GetCurrentProcess ().MainModule.FileName;
            var _processName = Path.GetFileNameWithoutExtension (_currentProcessFileName);
            if (Process.GetProcessesByName (_processName).ToList ().Exists (_process =>
                    _process.MainModule.FileName == _currentProcessFileName &&
                    _process.Id != Process.GetCurrentProcess ().Id
                )) {
                return;
            }
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            Application.Run (new DaemonKit ());
        }
    }
}