using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace RES_Timekeeper
{
    public partial class MainForm : Form
    {
        private TimePeriodManager _periodManager = new TimePeriodManager((OptionsForm.TimeTrackingLevels)(Properties.Settings.Default.TimeInterval));
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
            _trayMenu.MenuItems.Add("Options...", OnOptions);
            _trayMenu.MenuItems.Add("Help", OnHelp);
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
            var newItem = _periodManager.TimerTick(string.Empty);
            if (newItem != null)
            {
                _itemForm.DisplayNewUnconfirmedItem(newItem);
            }
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
            var form = new WorkorderForm();
            form.Show();
        }

        private void OnEditTimes(object sender, EventArgs e)
        {
            var form = new EditDayForm();
            form.Show();
        }


        private void OnSetAllToLunch(object sender, EventArgs e)
        {
            _itemForm.SetAllUnconfirmedToLunch();
        }

        private void OnDisplaySummary(object sender, EventArgs e)
        {
            var form = new SummaryForm();
            form.ShowDialog();
        }

        private void OnPause(object sender, EventArgs e)
        {
            _periodManager.Paused = !_periodManager.Paused;
            _pauseMenuItem.Checked = _periodManager.Paused;
        }

        private void OnOptions(object sender, EventArgs e)
        {
            var form = new OptionsForm();
            form.SelectedTimeTrackingLevel = (OptionsForm.TimeTrackingLevels)(Properties.Settings.Default.TimeInterval);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _periodManager.TimingInterval = form.SelectedTimeTrackingLevel;
                Properties.Settings.Default.TimeInterval = (int)(form.SelectedTimeTrackingLevel);
                Properties.Settings.Default.Save();
            }
        }

        private void OnHelp(object sender, EventArgs e)
        {
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(folder, "UserGuide.chm");
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, we couldn't open the Help. We tried to open the file {path}, but we got the error [{ex.Message}].");
            }
        }

        private void OnAbout(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void OnExit(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to exit? Really??", "Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
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
