using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RES_Timekeeper.Data;

namespace RES_Timekeeper
{
    public partial class AddItemForm : Form
    {
        private DateTime _selectedDay;

        public AddItemForm(DateTime selectedDay)
        {
            InitializeComponent();

            _selectedDay = selectedDay;

            _dtStart.Value = _selectedDay.Date.AddHours(9);
            _dtStart.MinDate = _selectedDay.Date;
            _dtStart.MaxDate = _selectedDay.Date.AddMinutes(23 * 60 + 59);

            _dtEnd.Value = _selectedDay.Date.AddHours(10);
            _dtEnd.MinDate = _selectedDay.Date.AddMinutes(1);
            _dtEnd.MaxDate = _selectedDay.Date.AddHours(24);

            CheckTimesForOverlap();
        }

        private void CheckTimesForOverlap()
        {
            bool overlap = EditItemForm.CheckTimesForOverlap(_dtStart.Value, _dtEnd.Value);
            _lblOverlapWarning.Visible = overlap;
            _btnOK.Enabled = !overlap && !string.IsNullOrEmpty(_lblProjectCode.Text);
        }

        private void _dtStart_ValueChanged(object sender, EventArgs e)
        {
            if (_dtStart.Value >= _dtEnd.Value)
                _dtStart.Value = _dtEnd.Value.AddMinutes(-1);

            CheckTimesForOverlap();
        }

        private void _dtEnd_ValueChanged(object sender, EventArgs e)
        {
            if (_dtEnd.Value <= _dtStart.Value)
                _dtEnd.Value = _dtStart.Value.AddMinutes(1);

            CheckTimesForOverlap();
        }

        public string Notes
        {
            get { return _tbNotes.Text; }
        }

        public DateTime ItemStart
        {
            get {return _dtStart.Value;}
        }

        public DateTime ItemEnd
        {
            get {return _dtEnd.Value;}
        }

        public int ProjectID
        {
            get { return (int)_lblProjectCode.Tag; }
        }
    
        private void _btnProjectBrowse_Click(object sender, EventArgs e)
        {
            WorkorderSelector frm = new WorkorderSelector();
            ProjectList projects = ProjectList.Load(true);
            frm.Initialise(projects);
            frm.Owner = this;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _lblProjectCode.Text = frm.SelectedProject.Code;
                _lblProjectCode.Tag = frm.SelectedProject.ID;
                CheckTimesForOverlap();
            }        
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            Item newItem = Item.CreateNewItem(ItemStart, ItemEnd);
            newItem.Confirmed = true;
            newItem.ProjectID = ProjectID;
            newItem.Notes = Notes;
            newItem.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
