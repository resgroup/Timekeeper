using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

using mshtml;




namespace RES_Timekeeper
{
    public partial class FillAgressoForm : Form
    {
        private string[,] _textItems;

        public FillAgressoForm(string[,] textItems)
        {
            InitializeComponent();

            for (int col = 0; col < textItems.GetLength(1) + 1; col++)
            {
                int newColIndex = _dgvText.Columns.Add(col.ToString(), string.Empty);
                _dgvText.Columns[newColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            _dgvText.Columns[_dgvText.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int row = 0; row < textItems.GetLength(0); row++)
            {
                string[] rowItems = new string[textItems.GetLength(1) + 1];
                for (int col = 0; col < textItems.GetLength(1); col++)
                    rowItems[col] = textItems[row, col];
                rowItems[textItems.GetLength(1)] = "";
                _dgvText.Rows.Add(rowItems);
            }

            _textItems = textItems;
        }


        private void FillAgressoForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                for (int i = 0; i < _dgvText.Rows.Count; i++)
                {
                    _dgvText.Rows[i].Cells[_dgvText.Columns.Count - 1].Value = string.Empty;
                }
                FillAgresso();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error filling Agresso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void FillAgresso()
        {
            IHTMLDocument3 doc3 = null;
            try
            {
                doc3 = InternetExplorerUtilities.GetTimesheetDocument();
                if (doc3 == null)
                {
                    MessageBox.Show("Can't find Agresso timesheet page. Please check and try again", "Can't find page", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Now add each line in turn
                for (int row = 0; row < _textItems.GetLength(0); row++)
                {
                    Color originalColour = _dgvText.Rows[row].DefaultCellStyle.BackColor;
                    _dgvText.Rows[row].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    string[] rowItems = new string[_textItems.GetLength(1)];
                    for (int col = 0; col < _textItems.GetLength(1); col++)
                        rowItems[col] = _textItems[row, col];

                    try
                    {
                        ProcessLine(row, rowItems, doc3);
                        _dgvText.Rows[row].Cells[_dgvText.Rows[row].Cells.Count - 1].Value = "✔";
                    }
                    catch (Exception ex)
                    {
                        _dgvText.Rows[row].Cells[_dgvText.Rows[row].Cells.Count - 1].Value = "✖ " + ex.Message;
                    }
                    _dgvText.Rows[row].DefaultCellStyle.BackColor = originalColour;
                    System.Threading.Thread.Sleep(500);
                }
            }
            finally
            {
                if (doc3 != null)
                    Marshal.ReleaseComObject(doc3);
            }
        }


        /// <summary>
        /// Inserts a line of items, the first two of which should be project code and description
        /// </summary>
        public void ProcessLine(int rowIndex, string[] items, IHTMLDocument3 doc3)
        {
            InternetExplorerUtilities.SelectAgressoLine(items[0], items[1], doc3);
            InternetExplorerUtilities.PushToSelectedLine(rowIndex, items, doc3, _dgvText);
        }
    }
}