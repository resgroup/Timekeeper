using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;


namespace RES_Timekeeper
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }


        public override void Install(IDictionary stateSaver)
        {
            bool alreadyRunning = TimeKeeperRunning();
            while (alreadyRunning)
            {
                MessageBox.Show("RES-Timekeeper is already running. Please exit it by right-clicking on it's icon and choosing Quit from the menu", "RES-Timekeeper already running", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                
                alreadyRunning = TimeKeeperRunning();
            }

            base.Install(stateSaver);
        }

        private bool TimeKeeperRunning()
        {
            return Process.GetProcesses().Where(x => x.ProcessName == "RES-Timekeeper").Count() > 0;
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\RES-Timekeeper.exe");
        }
    }
}
