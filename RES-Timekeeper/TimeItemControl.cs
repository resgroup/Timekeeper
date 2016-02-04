using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RES_Timekeeper.Base;
using RES_Timekeeper.Data;

namespace RES_Timekeeper
{
    public partial class TimeItemControl : UserControl
    {
        public class ItemChangedEventArgs : EventArgs
        {
        }

        public TimeItemControl()
        {
            InitializeComponent();

            Item newItem = Item.CreateNewItem(DateTime.Now, DateTime.Now.AddMinutes(1));

            _lblTime.Text = newItem.StartTime.ToString("HH:mm") + " - " + newItem.EndTime.ToString("HH:mm");
        }

        public TimeItemControl(Item newItem)
        {
            InitializeComponent();

            _lblTime.Text = newItem.StartTime.ToString("HH:mm") + " - " + newItem.EndTime.ToString("HH:mm");
            this.Tag = newItem;
        }

        private void _btnProjectSelect_Click(object sender, EventArgs e)
        {
            WorkorderSelector frm = new WorkorderSelector();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //((Item)(this.Tag)).ProjectCode = frm.SelectedProject.Code;
            }
        }
    }
}
