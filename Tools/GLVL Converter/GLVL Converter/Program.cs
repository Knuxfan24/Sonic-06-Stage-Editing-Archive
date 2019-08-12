using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using HedgeLib.Sets;

namespace GLvl_Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //WritePrerequisites();

            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            //}
            //catch
            //{
            //    WritePrerequisites();
            //}
        }

        public static void WritePrerequisites()
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, "HedgeLib.dll")))
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(Application.StartupPath, "HedgeLib.dll"), Properties.Resources.HedgeLib);
                    MessageBox.Show("HedgeLib.dll was written to the application path.", "GLvl Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Failed to write HedgeLib.dll. Please reinstall GLvl Converter.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            }

            if (!File.Exists(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll")))
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll"), Properties.Resources.Ookii_Dialogs);
                    MessageBox.Show("Ookii.Dialogs.dll was written to the application path.", "GLvl Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Failed to write Ookii.Dialogs.dll. Please reinstall GLvl Converter.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            }

            Application.Exit();
        }
    }
}
