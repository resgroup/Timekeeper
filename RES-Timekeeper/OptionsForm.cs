using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RES_Timekeeper
{
    public partial class OptionsForm : Form
    {
        public enum TimeTrackingLevels { M15, M30, H1, M1};

        public OptionsForm()
        {
            InitializeComponent();

            _cbTimeLevels.Items.Clear();
            _cbTimeLevels.Items.Add("15 Minutes");
            _cbTimeLevels.Items.Add("30 Minutes");
            _cbTimeLevels.Items.Add("1 Hour");
#if DEBUG
            _cbTimeLevels.Items.Add("1 Minute (DEBUG)");
#endif
        }

        public TimeTrackingLevels SelectedTimeTrackingLevel
        {
            get
            {
                return (TimeTrackingLevels)(_cbTimeLevels.SelectedIndex);
            }

            set
            {
                _cbTimeLevels.SelectedIndex = (int)value;
            }
        }
    }
}
