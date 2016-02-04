using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RES_Timekeeper.Data;
using mshtml;
using System.Runtime.InteropServices;

namespace RES_Timekeeper
{
    public partial class WorkorderForm : Form
    {
        private string _lastProjectCode = "blank";

        public WorkorderForm()
        {
            ProjectList projects = ProjectList.Load(false, true);

            InitializeComponent();
            projectListBindingSource.DataSource = projects;
            Projects = projects;
        }


        private ProjectList Projects
        {
            get;
            set;
        }


        private void _btnSave_Click(object sender, EventArgs e)
        {
            Projects.Save();
            DialogResult = DialogResult.OK;
            this.Close();
        }


        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void _dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < _dataGridView.RowCount; i++)
            {
                if (_dataGridView.Rows[i].Cells[1].Value!= null  &&  _dataGridView.Rows[i].Cells[1].Value.ToString() == "LUNCH")
                    _dataGridView.Rows[i].Cells[1].ReadOnly = true;
            }
        }


        private void ProjectForm_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Left = Owner.Right - this.Width;
                this.Top = Owner.Bottom - this.Height;
            }
        }


        private void _btnGetFromAgresso_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "RES-Timekeeper will attempt to fetch new workorder codes and descriptions from Agresso. Note that only new workorders will be added. You will have to manually transfer any other changes.\n\nPlease open the Agresso website and navigate to the Timesheet Entry page before clicking OK.", "Get new workorders from Agresso", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            IHTMLDocument3 doc3 = null;
            try
            {
                doc3 = IE8.GetTimesheetDocument();
                if (doc3 == null)
                {
                    MessageBox.Show("Can't find Agresso timesheet page. Please check and try again", "Can't find page", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    IEnumerable<string> currentProjectCodes = Projects.Projects.Select(p => p.Code.ToUpper());
                    IEnumerable<Tuple<string, string>> allCodesAndDescriptions = IE8.GetProjectCodesAndDescriptions(doc3);
                    List<Tuple<string, string>> toAdd = new List<Tuple<string, string>>();
            
                    if (currentProjectCodes.Count() == 0)
                    {
                        toAdd.AddRange(allCodesAndDescriptions);
                    }
                    else
                    {
                        foreach (var project in allCodesAndDescriptions)
                        {
                            if (!currentProjectCodes.Contains(project.Item1))
                                toAdd.Add(project);
                        }
                    }

                    foreach (var newProject in toAdd)
                    {
                        Project p = new Project();
                        p.Code = newProject.Item1;
                        p.Title = newProject.Item2;
                        Projects.Projects.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when getting workorders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (doc3 != null)
                    Marshal.ReleaseComObject(doc3);
            }
        }

        private void _dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == codeDataGridViewTextBoxColumn.Index)
            {
                if (_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ||
                    string.IsNullOrWhiteSpace(_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    _dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _lastProjectCode;
                }
            }
        }

        private void _dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == codeDataGridViewTextBoxColumn.Index)
            {
                _lastProjectCode = _dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }
    }
}
