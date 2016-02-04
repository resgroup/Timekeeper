using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Data
{
    public class ItemData
    {
        public ItemData(int projectID, DateTime startTime, DateTime endTime, bool confirmed, string notes)
        {
            ProjectID = projectID;
            StartTime = startTime;
            EndTime = endTime;
            Confirmed = confirmed;
            Notes = notes;
        }

        public int ProjectID
        {
            get;
            private set;
        }

        public DateTime StartTime
        {
            get;
            private set;
        }

        public DateTime EndTime
        {
            get;
            private set;
        }

        public bool Confirmed
        {
            get;
            private set;
        }

        public string Notes
        {
            get;
            private set;
        }       
    }
}
