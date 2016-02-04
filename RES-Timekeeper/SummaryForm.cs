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
    public partial class SummaryForm : Form
    {
        private static DateTime _formMonday = CalculateInitialMonday();
        private ProjectList _projectCache = ProjectList.Load(false);
        private int _rowIndexTotal;
        private static DataGridViewColumn _lastSortColumn = null;

        public SummaryForm()
        {
            InitializeComponent();
            FillProjectTimes();
        }

        private string[,] GetProjectTimes()
        {
            List<Tuple<int, string, string>> projectIdCodeTitle;
            List<Tuple<int, double>>[] projectHoursInDayAndWeek;
            GetProjectHoursInWeek(out projectIdCodeTitle, out projectHoursInDayAndWeek);

            string[,] textItems = new string[projectIdCodeTitle.Count+2, 10];
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            double[] dailyTotals = new double[7];
            int row = 0;
            foreach (var project in projectIdCodeTitle)
            {
                double weeklyTotal = 0;

                textItems[row, 0] = project.Item2;
                textItems[row, 1] = project.Item3;
                for (int i = 0; i < 7; i++)
                {
                    foreach (var projecthours in projectHoursInDayAndWeek[i])
                    {
                        if (projecthours.Item1 == project.Item1)
                        {
                            textItems[row, 2 + i] = String.Format(culture, "{0:0.0}", projecthours.Item2);
                            dailyTotals[i] += projecthours.Item2;
                            weeklyTotal += projecthours.Item2;
                        }
                    }
                }

                textItems[row, 9] = String.Format(culture, "{0:0.0}", weeklyTotal);
                row += 1;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    textItems[row+i,j] = string.Empty;
                }
            }
            row = row + 1;
            textItems[row, 0] = "Total";
            for (int i = 0; i < 7; i++)
            {
                textItems[row, 2 + i] = String.Format(culture, "{0:0.0}", dailyTotals[i]);
            }
            textItems[row, 9] = String.Format(culture, "{0:0.0}", dailyTotals.Sum());

            return textItems;
        }

        private void FillProjectTimes()
        {
            string[,] textItems = GetProjectTimes();
            _dgvProjectWork.Rows.Clear();
            SetColumnHeaders();

            for (int i = 0; i < textItems.GetLength(0); i++)
            {
                _dgvProjectWork.Rows.Add(new string[10] { textItems[i,0], textItems[i,1], textItems[i,2], textItems[i,3], textItems[i,4], textItems[i,5], textItems[i,6], textItems[i,7], textItems[i,8], textItems[i,9]});
            }
            _rowIndexTotal = textItems.GetLength(0)-1;

            foreach (DataGridViewCell c in _dgvProjectWork.Rows[_rowIndexTotal].Cells)
                c.Style.Font = _dgvProjectWork.Columns[9].DefaultCellStyle.Font;
        }

        private void GetProjectHoursInWeek(out List<Tuple<int, string, string>> projectIdCodeTitle, out List<Tuple<int, double>>[] projectHoursInDayAndWeek)
        {
            // Get list of all projects in the week, sorted by Code
            ItemList weeksItems = ItemList.Load(_formMonday, _formMonday.AddDays(7));
            var projectIDs = (from i in weeksItems.Items select i.ProjectID).Distinct();
            projectIdCodeTitle = new List<Tuple<int, string, string>>();
            foreach (int id in projectIDs)
            {
                if (id > 1) // Skip lunch
                {
                    Project p = _projectCache.Projects.Where(proj => proj.ID == id).First();
                    projectIdCodeTitle.Add(new Tuple<int, string, string>(p.ID, p.Code, p.Title));
                }
            }
            projectIdCodeTitle.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            int overlap = weeksItems.TotalTimeOverlapSeconds;
            if (overlap > 600)
            {
                _panelWarning.Visible = true;
                _lblWarning.Text = "There are overlapping times totalling " + (overlap / 60).ToString() + " minutes in this week.\r\nPlease take this into account when entering times into Agresso.";
            }
            else
                _panelWarning.Visible = false;

            // Fill the weeks items grid
            _dgvWeeksItems.Rows.Clear();
            foreach (Item i in weeksItems.Items)
            {
                if (i.Confirmed)
                {
                    string[] items = new string[4];
                    items[0] = i.StartTime.ToString("MMM-dd HH:mm");
                    items[1] = i.EndTime.ToString("MMM-dd HH:mm");
                    items[2] = projectIdCodeTitle.Where(p => p.Item1 == i.ProjectID).Select(p => p.Item2).FirstOrDefault();
                    items[3] = i.Notes;
                    _dgvWeeksItems.Rows.Add(items);
                }
            }

            // Now get the hours by day for each project.
            projectHoursInDayAndWeek = new List<Tuple<int, double>>[7];
            for (int i = 0; i < 7; i++)
            {
                projectHoursInDayAndWeek[i] = new List<Tuple<int, double>>();
                DateTime dayStart = _formMonday.AddDays(i);
                DateTime dayEnd = dayStart.AddDays(1);
                IEnumerable<Item> itemsInDay = weeksItems.Items.Where(item => item.StartTime >= dayStart && item.EndTime < dayEnd);
                foreach (Tuple<int, string, string> projects in projectIdCodeTitle)
                {
                    IEnumerable<Item> projectItemsInDay = itemsInDay.Where(item => item.ProjectID == projects.Item1);
                    projectHoursInDayAndWeek[i].Add(new Tuple<int, double>(projects.Item1, projectItemsInDay.Sum(item => (item.EndTime - item.StartTime).TotalHours)));
                }
            }
        }

        private static DateTime CalculateInitialMonday()
        {
            DateTime d = DateTime.Now.Date;

            while (d.DayOfWeek != DayOfWeek.Monday)
                d = d.AddDays(-1);

            return d;
        }

        private void SetColumnHeaders()
        {
            string format = "d MMM"; // "ddd d MMM"
            _colMonday.HeaderText = _formMonday.ToString(format);
            _colTuesday.HeaderText = _formMonday.AddDays(1).ToString(format);
            _colWednesday.HeaderText = _formMonday.AddDays(2).ToString(format);
            _colThursday.HeaderText = _formMonday.AddDays(3).ToString(format);
            _colFriday.HeaderText = _formMonday.AddDays(4).ToString(format);
            _colSaturday.HeaderText = _formMonday.AddDays(5).ToString(format);
            _colSunday.HeaderText = _formMonday.AddDays(6).ToString(format);
        }

        private void _dgvProjectWork_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex > 1)
            {
                if (e.RowIndex < _rowIndexTotal)
                {
                    if (_dgvProjectWork.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                        _dgvProjectWork.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0 && 
                        _cbRound.Checked  )
                    {
                        double val = System.Convert.ToDouble(_dgvProjectWork.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        val = val * 4;
                        val = Math.Round(val);
                        val = val / 4.0;
                        e.Value = val.ToString("0.00");
                        e.FormattingApplied = true;
                    }
                }
                else
                {
                    double total = 0;
                    for (int i = 0; i < _rowIndexTotal; i++)
                    {
                        if (_dgvProjectWork.Rows[i].Cells[e.ColumnIndex].Value != null &&
                            _dgvProjectWork.Rows[i].Cells[e.ColumnIndex].Value.ToString().Length > 0)
                        {
                            total += System.Convert.ToDouble(_dgvProjectWork.Rows[i].Cells[e.ColumnIndex].Value);
                        }
                    }
                    if (_cbRound.Checked)
                    {
                        total = total * 4;
                        total = Math.Round(total);
                        total = total / 4.0;
                    }
                    e.Value = total.ToString("0.00");
                    e.FormattingApplied = true;
                }
            }
        }

        private void _cbRound_CheckedChanged(object sender, EventArgs e)
        {
            _dgvProjectWork.Refresh();
        }

        private void _dgvProjectWork_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (_lastSortColumn != e.Column)
            {
                if (e.RowIndex1 >= _rowIndexTotal - 1 || e.RowIndex2 >= _rowIndexTotal - 1)
                    e.SortResult = (e.RowIndex1 - e.RowIndex2);
                else
                    e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
            }
            else
            {
                if (e.RowIndex1 >= _rowIndexTotal - 1 || e.RowIndex2 >= _rowIndexTotal - 1)
                    e.SortResult = (e.RowIndex2 - e.RowIndex1);
                else
                    e.SortResult = string.Compare(e.CellValue2.ToString(), e.CellValue1.ToString());
            }

            _lastSortColumn = e.Column;
            e.Handled = true;
        }

        private void _btnLeft_Click(object sender, EventArgs e)
        {
            _formMonday = _formMonday.AddDays(-7);
            FillProjectTimes();
        }

        private void _btnRight_Click(object sender, EventArgs e)
        {
            _formMonday = _formMonday.AddDays(7);
            FillProjectTimes();
        }

        private void _btnFillAgresso_Click(object sender, EventArgs e)
        {
            _cbRound.Checked = true;
            MessageBox.Show("Please note that the automatic sending of times to Agresso is VERY experimental. It may fail or even send the wrong numbers. Do please check the numbers in Agresso before sumbitting the week.\n\nPlease ensure that you have selected the correct week in Agresso Timesheet Entry before clicking OK", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            string[,] textItems = new string[_dgvProjectWork.RowCount - 2, _dgvProjectWork.ColumnCount - 1];
            for (int row = 0; row < _dgvProjectWork.RowCount - 2; row++)
            {
                for (int col = 0; col < _dgvProjectWork.ColumnCount - 1; col++)
                {
                    textItems[row, col] = _dgvProjectWork.Rows[row].Cells[col].FormattedValue.ToString(); ;
                }
            }

            try
            {
                FillAgressoForm frm = new FillAgressoForm(textItems);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A serious error occured when trying to fill the Agresso timesheet\n" + ex.Message, "Error when filling timesheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
