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
    public partial class EditDayForm : Form
    {
        DateTime _currentDisplayDay;
        DateTime _lastDisplayDay;
        DateTime _firstDisplayDay;
        ItemList CurrentItems { get; } = new ItemList();
        ProjectList _cachedProjects = ProjectList.Load(false);

        public EditDayForm()
        {
            InitializeComponent();

            _itemListBindingSource.DataSource = CurrentItems;

            DataService db = new DataService();
            try
            {
                _lastDisplayDay = db.GetMostRecentItem().EndTime.Date;
                _firstDisplayDay = db.GetLeastRecentItem().StartTime.Date;
            }
            catch
            {
                _lastDisplayDay = DateTime.Now.Date;
                _firstDisplayDay = DateTime.Now.Date;
            }

            _currentDisplayDay = _lastDisplayDay;
            DisplayDay(_currentDisplayDay);
        }

        private void DisplayDay(DateTime date)
        {
            var items = ItemList.Load(date.Date, date.Date.AddDays(1));
            var selectedItems = items.Items.Where(i => i.Confirmed).ToList();

            CurrentItems.Items.Clear();
            selectedItems.ForEach(i => CurrentItems.Items.Add(i));

            _lblDate.Text = _currentDisplayDay.ToString("dddd, d MMM yyyy");
        }

        private void _btnLeft_Click(object sender, EventArgs e)
        {
            if (_currentDisplayDay > _firstDisplayDay)
            {
                SaveItems();
                _btnRight.Enabled = true;
                _currentDisplayDay = _currentDisplayDay.AddDays(-1);
                DisplayDay(_currentDisplayDay);
            }

            if (_currentDisplayDay <= _firstDisplayDay)
            {
                _currentDisplayDay = _firstDisplayDay;
                _btnLeft.Enabled = false;
            }
        }

        private void _btnRight_Click(object sender, EventArgs e)
        {
            if (_currentDisplayDay < _lastDisplayDay)
            {
                SaveItems();
                _btnLeft.Enabled = true;
                _currentDisplayDay = _currentDisplayDay.AddDays(1);
                DisplayDay(_currentDisplayDay);
            }

            if (_currentDisplayDay >= _lastDisplayDay)
            {
                _currentDisplayDay = _lastDisplayDay;
                _btnRight.Enabled = false;
            }
        }

        private void _dgvItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewButtonCell projectButton = _dgvItems.Rows[e.RowIndex].Cells[colProjButton.Index] as DataGridViewButtonCell;
            projectButton.Value = "...";

            SetProjectCodesFromIDs();
        }

        private void SetProjectCodesFromIDs()
        {
            for (int i = 0; i < _dgvItems.Rows.Count; i++)
            {
                Project p = _cachedProjects.GetFromID(CurrentItems.Items[i].ProjectID);
                _dgvItems.Rows[i].Cells[projectCodeDataGridViewTextBoxColumn.Index].Value = p.Code + "  " + p.Title;
                _dgvItems.Rows[i].Cells[projectCodeDataGridViewTextBoxColumn.Index].ToolTipText = p.Title;
            }
        }

        private void HandleProjectButtonClick(DataGridViewCellEventArgs e)
        {
            ProjectList projects = ProjectList.Load(true);

            WorkorderSelector frm = new WorkorderSelector(projects);
            frm.Owner = this;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CurrentItems.Items[e.RowIndex].ProjectID = frm.SelectedProject.Id;

                _dgvItems.Refresh();
                _cachedProjects = ProjectList.Load(false);
                SetProjectCodesFromIDs();
            }
        }

        private void SaveItems()
        {
            CurrentItems.Items.ToList()
                    .ForEach(item => item.Save());
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            AddItemForm frm = new AddItemForm(_currentDisplayDay);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DisplayDay(_currentDisplayDay);
            }
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            SaveItems();
            this.Close();
        }

        private void _dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == startTimeDataGridViewTextBoxColumn.Index ||
                 e.ColumnIndex == endTimeDataGridViewTextBoxColumn.Index) &&
                _dgvItems.Rows[e.RowIndex].DataBoundItem is Item)
            {
                var editItem = _dgvItems.Rows[e.RowIndex].DataBoundItem as Item;
                var frm = new EditItemForm(editItem);
                frm.ShowDialog();
            }
        }

        private void _dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colProjButton.Index)
            {
                HandleProjectButtonClick(e);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var items = _dgvItems.SelectedRows
                                .OfType<DataGridViewRow>()
                                .Select(row => row.DataBoundItem)
                                .OfType<Item>()
                                .ToList();

            items.ForEach(item => item.MarkDeleted());
            this.SaveItems();
            DisplayDay(_currentDisplayDay);
        }
    }
}
