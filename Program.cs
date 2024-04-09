using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Added by Visual Studio 2022
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            // Things I steal from Microsoft docs and StackOverflow about system info
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); //https://stackoverflow.com/questions/1678771/how-to-get-an-array-of-months-in-c-sharp
        }
    }
}
