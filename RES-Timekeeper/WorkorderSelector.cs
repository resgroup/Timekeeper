﻿using System;
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
    public partial class WorkorderSelector : Form
    {
        private Project _selectedProject = null;
        private ProjectList _allProjects = null;
        private ProjectList _recentProjects = null;

        public WorkorderSelector(ProjectList allProjects)
        {
            _allProjects = allProjects;
            _recentProjects = ProjectList.LoadRecentlyUsed();

            InitializeComponent();

            if (_recentProjects.Projects.Count > 0)
            {
                _rbShowRecent.Checked = true;
            }
            else
            {
                _rbShowAll.Checked = true;
            }

            _dgvProjects.SortCompare += new DataGridViewSortCompareEventHandler(_dgvProjects_SortCompare);
        }

        void _dgvProjects_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            string a = e.CellValue1.ToString();
            string b = e.CellValue2.ToString();

            if (_dgvProjects.SortedColumn.Index == colProjectCode.Index)
            {
                try
                {
                    e.SortResult = ProjectCodeSort(a, b, _dgvProjects.SortOrder);
                    e.Handled = true;
                    return;
                }
                catch { }
            }

            e.SortResult = string.Compare(a, b);
            e.Handled = true;
        }


        public static int ProjectCodeSort(string a, string b, SortOrder s)
        {
            if (a == "LUNCH" || b == "LUNCH")
            {
                if (a == "LUNCH")
                    return -1;
                else
                    return 1;
            }

            var toksA = a.Split(new char[1] { '-' })
                    .Select(ss =>
                    {
                        int i;
                        int.TryParse(ss, out i);
                        return i;
                    }).ToList(); ;

            var toksB = b.Split(new char[1] { '-' })
                    .Select(ss =>
                    {
                        int i;
                        int.TryParse(ss, out i);
                        return i;
                    }).ToList(); ;

            for (int i = 0; i < 3; i++)
            {
                if (toksA[i] != toksB[i])
                {
                    return toksA[i] - toksB[i];
                }
            }

            return 0;
        }


        public Project SelectedProject
        {
            get { return _selectedProject; }
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            if (_dgvProjects.SelectedRows.Count == 1)
            {
                _selectedProject = (Project)_dgvProjects.SelectedRows[0].Tag;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void _dgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedProject = (Project)_dgvProjects.Rows[e.RowIndex].Tag;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }


        private void _dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            this._btnOK.Enabled = (_dgvProjects.SelectedRows.Count > 0);
        }


        private void _btnEdit_Click(object sender, EventArgs e)
        {
            WorkorderForm frm = new WorkorderForm();
            frm.Owner = this;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _allProjects = ProjectList.Load(true);
                _rbShowAll.Checked = true;
                RepopulateGrid();
            }
        }


        private void ProjectSelector_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Left = Owner.Right - this.Width;
                this.Top = Owner.Bottom - this.Height;
            }
        }


        private void _dgvProjects_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            bool alphaSort = _dgvProjects.SortedColumn.Index == colProjectTitle.Index;
            Properties.Settings.Default.SortWorkOrdersAlphabetically = alphaSort;
            Properties.Settings.Default.Save();
        }


        private void _dgvProjects_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool alphaSort = _dgvProjects.SortedColumn.Index == colProjectTitle.Index;
            Properties.Settings.Default.SortWorkOrdersAlphabetically = alphaSort;
            Properties.Settings.Default.Save();
        }


        private void FillGridWithArchived()
        {
            IList<Project> archivedProjects = _allProjects.Projects.Where(p => !p.Visible).ToList();
            AddProjectToGrid(archivedProjects);
        }

        private void FillGridWithAll()
        {
            IList<Project> visibleProjects = _allProjects.Projects.Where(p => p.Visible).ToList();
            AddProjectToGrid(visibleProjects);
        }

        private void FillGridWithRecent()
        {
            AddProjectToGrid(_recentProjects.Projects);
        }

        private void AddProjectToGrid(IList<Project> projects)
        {
            _dgvProjects.Rows.Clear();

            foreach (var p in projects)
            {
                if (PassesFilter(p))
                {
                    object[] rowVals = new object[2] { p.Code, p.Title };
                    int index = _dgvProjects.Rows.Add(rowVals);
                    _dgvProjects.Rows[index].Tag = p;
                }
            }

            if (Properties.Settings.Default.SortWorkOrdersAlphabetically)
                _dgvProjects.Sort(colProjectTitle, ListSortDirection.Ascending);
            else
                _dgvProjects.Sort(colProjectCode, ListSortDirection.Ascending);
        }

        private bool PassesFilter(Project p)
        {
            if (_tbFilter.TextLength > 0)
            {
                string filterL = _tbFilter.Text.ToLower();
                string projectL = p.Title.ToLower();
                return projectL.Contains(filterL);
            }
            else
                return true;
        }


        private void _rbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            RepopulateGrid();
        }


        private void _rbShowRecent_CheckedChanged(object sender, EventArgs e)
        {
            RepopulateGrid();
        }


        private void _tbFilter_TextChanged(object sender, EventArgs e)
        {
            RepopulateGrid();
        }

        private void _rbShowArchived_CheckedChanged(object sender, EventArgs e)
        {
            RepopulateGrid();
        }

        private void RepopulateGrid()
        {
            if (_rbShowAll.Checked)
                FillGridWithAll();
            else if (_rbShowRecent.Checked)
                FillGridWithRecent();
            else
                FillGridWithArchived();
        }
    }
}
