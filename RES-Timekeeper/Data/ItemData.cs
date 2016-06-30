using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Data
{
    public class ItemData
    {
        public int ProjectID { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public bool Confirmed { get; }
        public string Notes { get; }
        public ItemData(int projectID, DateTime startTime, DateTime endTime, bool confirmed, string notes)
        {
            ProjectID = projectID;
            StartTime = startTime;
            EndTime = endTime;
            Confirmed = confirmed;
            Notes = notes;
        }
    }
}
