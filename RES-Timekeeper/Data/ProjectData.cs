using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Data
{
    public class ProjectData
    {
        public ProjectData(int id, string code, string title, bool visible)
        {
            ID = id;
            Code = code.Replace('\'', '`');
            Title = title.Replace('\'', '`');
            Visible = visible;
        }

        public int ID
        {
            get;
            private set;
        }

        public string Code
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public bool Visible
        {
            get;
            private set;
        }
    }
}
