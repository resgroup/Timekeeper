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
    public partial class EditItemForm : Form
    {
        private DateTime _selectedDay;
        private DateTime _originalStart;
        private DateTime _originalEnd;
        private Item _editItem;

        public EditItemForm(Item editItem)
        {
            InitializeComponent();

            _editItem = editItem;
            _selectedDay = _editItem.StartTime.Date;
            _originalStart = _editItem.StartTime;
            _originalEnd = _editItem.EndTime;

            _dtStart.Value = _editItem.StartTime.Date + new TimeSpan(_editItem.StartTime.Hour, _editItem.StartTime.Minute, 0);
            _dtStart.MinDate = _selectedDay.Date;
            _dtStart.MaxDate = _selectedDay.Date.AddMinutes(23 * 60 + 59);

            _dtEnd.Value = _editItem.EndTime.Date + new TimeSpan(_editItem.EndTime.Hour, _editItem.EndTime.Minute, 0);
            _dtEnd.MinDate = _selectedDay.Date.AddMinutes(1);
            _dtEnd.MaxDate = _selectedDay.Date.AddHours(24);

            ProjectList projects = ProjectList.Load(true);
            _lblProjectCode.Text = projects.GetFromID(_editItem.ProjectID).Code;
            _lblProjectCode.Tag = projects.GetFromID(_editItem.ProjectID).ID;

            _tbNotes.Text = _editItem.Notes;

            CheckTimesForOverlap();
        }

        private void CheckTimesForOverlap()
        {
            bool overlap = CheckTimesForOverlapNotCurrent(_dtStart.Value, _dtEnd.Value);
            _lblOverlapWarning.Visible = overlap;
            _btnOK.Enabled = !overlap && !string.IsNullOrEmpty(_lblProjectCode.Text);
        }


        public static bool CheckTimesForOverlap(DateTime userStart, DateTime userEnd)
        {
            ItemList daysItems = ItemList.Load(userStart.Date, userStart.Date.AddDays(1));

            bool overlap = false;
            foreach (var i in daysItems.Items)
            {
                if (i.StartTime >= userEnd || i.EndTime <= userStart)
                    overlap = false;
                else if (i.StartTime > userStart && i.EndTime < userEnd)
                    overlap = true;
                else if (i.StartTime > userStart && i.EndTime > userEnd)
                    overlap = true;
                else if (i.StartTime < userStart && i.EndTime < userEnd)
                    overlap = true;

                if (overlap)
                    break;
            }

            return overlap;
        }

        public bool CheckTimesForOverlapNotCurrent(DateTime userStart, DateTime userEnd)
        {
            ItemList daysItems = ItemList.Load(userStart.Date, userStart.Date.AddDays(1));
            foreach (var i in daysItems.Items)
            {
                if (i.StartTime == _originalStart  && i.EndTime == _originalEnd)
                {
                    daysItems.Items.Remove(i);
                    break;
                }
            }
            daysItems.RoundTimesToTheMinute();

            bool overlap = false;
            foreach (var i in daysItems.Items)
            {
                if (i.StartTime >= userEnd || i.EndTime <= userStart)
                    overlap = false;
                else if (i.StartTime > userStart && i.StartTime < userEnd)
                    overlap = true;
                else if (i.EndTime > userStart && i.EndTime < userEnd)
                    overlap = true;
                else if (i.StartTime < userStart && i.EndTime > userEnd)
                    overlap = true;

                if (overlap)
                    break;
            }

            return overlap;
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
            _editItem.StartTime = ItemStart;
            _editItem.EndTime = ItemEnd;
            _editItem.ProjectID = ProjectID;
            _editItem.Notes = Notes;
            _editItem.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
