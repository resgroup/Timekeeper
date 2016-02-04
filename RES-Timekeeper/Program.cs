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
                //if (IsUserAnAdministrator())
                //    CheckForNewVersion();

                MainForm mForm = new MainForm();
                Application.Run();
            }
        }


        private static void LogSession()
        {
            // Attempt to log the session, but don't complain if we're not on the RES Network
            try
            {
                SQLServerQueries.LogSession();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error in session logging\n" + ex.ToString(), "Session logging", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }


        /*private static void CheckForNewVersion()
        {
            // Attempt to see if there is a new version, but don't complain if we're not on the RES Network
            try
            {
                string availableVersion;
                string thisVersion;
                if (!SQLServerQueries.IsLatestVersion(out thisVersion, out availableVersion))
                {
                    if (MessageBox.Show("A newer version of RES-Timekeeper is available. Do you want to install it? Your version of Timekeeper is " + thisVersion + ", the newer version is " + availableVersion, "New version available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("X:\\Exe\\RES-Timekeeper\\RES-Timekeeper Installer.msi");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error in version checking\n" + ex.ToString(), "Session logging", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }*/

        /*private static bool IsUserAnAdministrator()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
            {
                UserPrincipal up = UserPrincipal.Current;
                GroupPrincipal gp = GroupPrincipal.FindByIdentity(pc, "S-1-5-32-544");
                if (up.IsMemberOf(gp))
                    return true;
            }
            return false;
        }*/
    }
}
