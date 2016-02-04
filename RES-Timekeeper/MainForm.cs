using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

using RES_Timekeeper.Data;
using System.Runtime.InteropServices;
using System.IO;

namespace RES_Timekeeper
{
    public partial class MainForm : Form
    {
        private TimePeriodManager _periodManager = new TimePeriodManager();
        private TimekeeperForm _itemForm;
        private MenuItem _pauseMenuItem;

        private NotifyIcon _trayIcon;
        private ContextMenu _trayMenu;


        public MainForm()
        {
            InitializeComponent();
            
            _itemForm = new TimekeeperForm();
            _itemForm.Visible = _itemForm.UnconfirmedItems.Items.Count > 0;

            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvents_SessionEnded);

            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Edit Times...", OnEditTimes);
            _trayMenu.MenuItems.Add("Edit Workorders...", OnEditWorkorders);
            _trayMenu.MenuItems.Add("Display Summary", OnDisplaySummary);
            _trayMenu.MenuItems.Add("Set all unconfirmed to LUNCH", OnSetAllToLunch);
            _pauseMenuItem = _trayMenu.MenuItems.Add("Pause", OnPause);
            _pauseMenuItem.Checked = false;
            _trayMenu.MenuItems.Add("-");
            _trayMenu.MenuItems.Add("Help", OnHelp);
            _trayMenu.MenuItems.Add("Raise Request", OnRaiseRequest);
            _trayMenu.MenuItems.Add("About RES-TimeKeeper", OnAbout);
            _trayMenu.MenuItems.Add("-");
            _trayMenu.MenuItems.Add("Quit", OnExit);

            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "RES-TimeKeeper";
            _trayIcon.Icon = Properties.Resources.App;
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
            _trayIcon.DoubleClick += new EventHandler(OnDisplaySummary);
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            Item newItem = _periodManager.TimerTick(string.Empty);
            if (newItem != null)
                _itemForm.DisplayNewUnconfirmedItem(newItem);
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Enabled = false;
            _periodManager.SessionEnding("System is shutting down");
            e.Cancel = false;
            _trayIcon.Visible = false;
        }



        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            _timer.Enabled = false;
            _periodManager.SessionEnding("System is shutting down");
            e.Cancel = false;
        }


        void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            _timer.Enabled = false;
            _periodManager.SessionEnding("System is shutting down");
        }


        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                _timer.Enabled = false;
                _periodManager.SessionEnding("System is being suspended or hibernated");
            }
            else if (e.Mode == PowerModes.Resume)
            {
                _periodManager.SessionResuming();
                _itemForm.DisplayUnconfirmedItems();
                _timer.Enabled = true;
            }
        }


        private void OnEditWorkorders(object sender, EventArgs e)
        {
            WorkorderForm frm = new WorkorderForm();
            frm.Show();
        }

        private void OnEditTimes(object sender, EventArgs e)
        {
            EditDayForm frm = new EditDayForm();
            frm.Show();
        }


        private void OnSetAllToLunch(object sender, EventArgs e)
        {
            _itemForm.SetAllUnconfirmedToLunch();
        }
        
        private void OnDisplaySummary(object sender, EventArgs e)
        {
            SummaryForm s = new SummaryForm();
            s.ShowDialog();
        }

        private void OnPause(object sender, EventArgs e)
        {
            _periodManager.Paused = !_periodManager.Paused;
            _pauseMenuItem.Checked = _periodManager.Paused;
        }

        private void OnHelp(object sender, EventArgs e)
        {
            string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = System.IO.Path.Combine(folder, "UserGuide.chm");

            System.Diagnostics.Process.Start(path);
        }

        private void OnRaiseRequest(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://kl-web-001/resSoftware/request_add.asp?softwareID=1310");
        }

        private void OnAbout(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to exit? Really??", "Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _timer.Enabled = false;
                _periodManager.SessionEnding("User chose to exit RES-Timekeeper");
                this.Close();
                _trayIcon.Visible = false;
                Application.Exit();
            }
        }
    }
}
