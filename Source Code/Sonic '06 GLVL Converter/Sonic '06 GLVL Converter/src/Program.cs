using System;
using System.Windows.Forms;

namespace Sonic_06_GLVL_Converter
{
    static class Program
    {
        public static string GlobalVersion = "Version 1.0";

        [STAThread]

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
