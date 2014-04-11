using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace EnergyInternational
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                string CurrentExe = Process.GetCurrentProcess().MainModule.FileName;
                switch (args[0])
                {
                    case "replace":
                        Console.WriteLine("Replacing old version...");
                        File.Copy(CurrentExe, args[1], true);
                        Console.WriteLine("Cleaning up...");
                        Process.Start(args[1], "delete \"" + CurrentExe + "\"");
                        break;
                    case "delete":
                        int c = 0, m=10;
                        while (File.Exists(args[1]) && c < m)
                        {
                            Console.WriteLine("Deleting old version (" + (++c) + "/" + m + ")...");
                            File.Delete(args[1]);
                        }
                        Console.WriteLine("Running new version...");
                        Process.Start(CurrentExe);
                        break;
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
