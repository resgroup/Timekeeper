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
        public DateTime SelectedDay { get; }
        public string Notes => _tbNotes.Text;
        public DateTime ItemStart => _dtStart.Value;
        public DateTime ItemEnd => _dtEnd.Value;
        public int ProjectID => (int)_lblProjectCode.Tag;

        public AddItemForm(DateTime selectedDay)
        {
            this.SelectedDay = selectedDay;

            InitializeComponent();

            _dtStart.Value = SelectedDay.Date.AddHours(9);
            _dtStart.MinDate = SelectedDay.Date;
            _dtStart.MaxDate = SelectedDay.Date.AddMinutes(23 * 60 + 59);

            _dtEnd.Value = SelectedDay.Date.AddHours(10);
            _dtEnd.MinDate = SelectedDay.Date.AddMinutes(1);
            _dtEnd.MaxDate = SelectedDay.Date.AddHours(24);

            CheckTimesForOverlap();
        }

        private void CheckTimesForOverlap()
        {
            _lblOverlapWarning.Visible = EditItemForm.CheckTimesForOverlap(_dtStart.Value, _dtEnd.Value);
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


        private void _btnProjectBrowse_Click(object sender, EventArgs e)
        {
            var projects = ProjectList.Load(false);
            WorkorderSelector frm = new WorkorderSelector(projects);
            frm.Owner = this;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _lblProjectCode.Text = frm.SelectedProject.Code;
                _lblProjectCode.Tag = frm.SelectedProject.Id;
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
