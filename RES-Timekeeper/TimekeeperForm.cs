using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RES_Timekeeper.Base;
using RES_Timekeeper.Data;
using System.Runtime.InteropServices;

namespace RES_Timekeeper
{
    public partial class TimekeeperForm : Form
    {
        ProjectList _cachedProjects;
        ItemList _unconfirmedItems;
        IntPtr _initialFocusForm = IntPtr.Zero;


        public TimekeeperForm()
        {
            _cachedProjects = ProjectList.Load(false);
            _unconfirmedItems = ItemList.Load(true);
            InitializeComponent();
            _itemListBindingSource.DataSource = UnconfirmedItems;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint="PostMessage")]
        public static extern int PostMessage(IntPtr WindowHandle, int Command, int Data, int ID);


        private void UpdateDisplay()
        {
            if (_unconfirmedItems.Items.Count > 0)
            {
                IntPtr currentFocus = IntPtr.Zero;
                if (!this.Visible)
                {
                    currentFocus = GetForegroundWindow();
                    this.Visible = true;
                }

                if (currentFocus != IntPtr.Zero)
                {
                    SetForegroundWindow(currentFocus);
                    PostMessage(currentFocus, 6, 1, 0);
                }
            }
            else
            {
                this.Visible = false;
            }
        }


        public void DisplayUnconfirmedItems()
        {
            ItemList newUnconfirmedItems = ItemList.Load(true);
            _unconfirmedItems.Items.Clear();
            foreach (var item in newUnconfirmedItems.Items)
                _unconfirmedItems.Items.Add(item);

            UpdateDisplay();
        }

        
        public void DisplayNewUnconfirmedItem(Item newItem)
        {
            _unconfirmedItems.Items.Insert(0, newItem);
            UpdateDisplay();
        }


        public ItemList UnconfirmedItems
        {
            get { return _unconfirmedItems; }
            //set { _unconfirmedItems.Items = value; }
        }


        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }


        // http://winsharp93.wordpress.com/2009/06/29/find-out-size-and-position-of-the-taskbar/
        public static int GetTaskbarHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }


        private void _dgvItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {            
            DataGridViewButtonCell projectButton = _dgvItems.Rows[e.RowIndex].Cells[colProjButton.Index] as DataGridViewButtonCell;
            projectButton.Value = "...";

            SetProjectCodesFromIDs();
            SetHeight();
            SetWidth();
        }


        private void SetHeight()
        {
            if (_dgvItems.Rows.Count > 0)
                this.Height = _dgvItems.Rows[0].Height * _unconfirmedItems.Items.Count + _dgvItems.ColumnHeadersHeight + 2;
            else
                this.Height = _dgvItems.ColumnHeadersHeight + 2;

            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
        }


        private void SetWidth()
        {
            int widestProjectCode = 120;
            using (Graphics graphics = Graphics.FromHwnd(this.Handle))
            {
                GraphicsUnit oldUnits = graphics.PageUnit;
                graphics.PageUnit = GraphicsUnit.Pixel;
                foreach (DataGridViewRow row in _dgvItems.Rows)
                {
                    string text = row.Cells[projectCodeDataGridViewTextBoxColumn.Index].Value.ToString();
                    SizeF size = graphics.MeasureString(text + "m", _dgvItems.DefaultCellStyle.Font);
                    widestProjectCode = Math.Max(widestProjectCode, (int)(size.Width+0.5));
                }
                graphics.PageUnit = oldUnits;
                widestProjectCode = Math.Min(widestProjectCode, 500);
            }

            int formWidth = this.Width - projectCodeDataGridViewTextBoxColumn.Width + widestProjectCode;
            this.Left = this.Right - formWidth;
            this.Width = formWidth;
            projectCodeDataGridViewTextBoxColumn.Width = widestProjectCode;
        }


        private void _dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == colProjButton.Index)
                {
                    HandleProjectButtonClick(e);
                }
                else if (e.ColumnIndex == confirmedDataGridViewCheckBoxColumn.Index)
                {
                    HandleConfirmedCheckboxClick(e);
                }
            }
        }


        private void _dgvItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == confirmedDataGridViewCheckBoxColumn.Index)
                HandleConfirmedCheckboxClick(e);            
        }


        private void HandleProjectButtonClick(DataGridViewCellEventArgs e)
        {
            WorkorderSelector frm = new WorkorderSelector();
            ProjectList projects = ProjectList.Load(true);
            frm.Initialise(projects);
            frm.Owner = this;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = e.RowIndex; i >= 0; i--)
                {
                    _unconfirmedItems.Items[i].ProjectID = frm.SelectedProject.ID;
                }

                _dgvItems.Refresh();
                _cachedProjects = ProjectList.Load(false);
                SetProjectCodesFromIDs();
                SetWidth();
            }
        }


        private void HandleConfirmedCheckboxClick(DataGridViewCellEventArgs e)
        {
            Item i = UnconfirmedItems.Items[e.RowIndex];
            bool newState = !(bool)(_dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            if (newState)
            {
                if (i.ProjectID < 1)
                {
                    _dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    _dgvItems.Refresh();
                }
                else
                {
                    UnconfirmedItems.Items.RemoveAt(e.RowIndex);
                    i.Confirmed = true;
                    i.Save();
                    SetHeight();

                    if (UnconfirmedItems.Items.Count == 0)
                        this.Visible = false;
                }
            }
        }


        private void _dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == startTimeDataGridViewTextBoxColumn.Index || e.ColumnIndex == endTimeDataGridViewTextBoxColumn.Index)
            {
                DateTime firstDate = ((DateTime)(_dgvItems.Rows[0].Cells[e.ColumnIndex].Value)).Date;
                bool allSameDay = true;
                foreach (DataGridViewRow r in _dgvItems.Rows)
                {
                    DateTime thisDate = ((DateTime)(r.Cells[e.ColumnIndex].Value)).Date;
                    if (thisDate != firstDate)
                    {
                        allSameDay = false;
                        break;
                    }
                }

                DateTime rowDate = ((DateTime)(_dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                if (allSameDay)
                    e.Value = rowDate.ToString("HH:mm");
                else
                    e.Value = rowDate.ToString("MMM-dd HH:mm");
                e.FormattingApplied = true;
            }
        }


        private void SetProjectCodesFromIDs()
        {
            for (int i = 0; i < _dgvItems.Rows.Count; i++)
            {
                Project p = _cachedProjects.GetFromID(UnconfirmedItems.Items[i].ProjectID);
                _dgvItems.Rows[i].Cells[projectCodeDataGridViewTextBoxColumn.Index].Value = p.Code + "  " + p.Title;
                _dgvItems.Rows[i].Cells[projectCodeDataGridViewTextBoxColumn.Index].ToolTipText = p.Title;
            }
        }

        public void SetAllUnconfirmedToLunch()
        {
            for (int i = UnconfirmedItems.Items.Count() - 1; i >= 0; i--)
            {
                var u = UnconfirmedItems.Items[i];
                u.ProjectID = Project.LUNCH_PROJECT_ID;
                u.Confirmed = true;
                u.Save();
                UnconfirmedItems.Items.RemoveAt(i);
                SetHeight();
                Application.DoEvents();
            }

            if (UnconfirmedItems.Items.Count == 0)
                this.Visible = false;
        }
    }
}
