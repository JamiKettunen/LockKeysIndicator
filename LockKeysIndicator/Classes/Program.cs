using System;
using System.Threading;
using System.Windows.Forms;

namespace LockKeysIndicator
{
    static class Program
    {
        private static Mutex mutex = new Mutex(true, "{A6A91E81-4E69-4728-8C35-5AC3E1D2EF53}"); // Application specific mutex

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true)) // Another instance could NOT be found => Start the program normally
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormCore());

                mutex.ReleaseMutex();
            }
            else { Environment.Exit(2); } // Exit with code 2 if another instance was found
        }
    }
}
