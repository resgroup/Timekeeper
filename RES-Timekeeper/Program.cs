using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;

internal static class Useful
{
}
namespace RES_Timekeeper
{
    static class Program
    {
        // see http://odetocode.com/blogs/scott/archive/2004/08/20/the-misunderstood-mutex.aspx
        private static string appGuid = "4280EEF4-F185-421B-83ED-1CE92ECAD110";

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    //MessageBox.Show("Instance already running");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // it looks like this line could be deleted, but it is required for some reason.
                // If you don't have, the icon doesn't appear in the system tray
                new MainForm(); 
                Application.Run();
            }
        }
    }
}
