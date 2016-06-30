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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, appGuid))
            {
#if DEBUG
#else
                if (!mutex.WaitOne(0, false))
                {
                    //MessageBox.Show("Instance already running");
                    return;
                }
#endif

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                LogSession();
                var mainForm = new MainForm();
                Application.Run();
            }
        }


        private static void LogSession()
        {
            // Attempt to log the session, but don't complain if we're not on the RES Network
            try
            {
                SessionLogger.Log("Server=KL-SQL-001;DataBase=RESSoftware;Integrated Security=SSPI", softwareId: 1310);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error in session logging\n" + ex.ToString(), "Session logging", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
    }
}
