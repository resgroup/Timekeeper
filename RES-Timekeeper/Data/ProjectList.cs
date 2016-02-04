using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using RES_Timekeeper.Base;

namespace RES_Timekeeper.Data
{
    public class ProjectList
    {
        public ProjectList()
        {
            Projects = new BindingList<Project>();
        }

        public BindingList<Project> Projects
        {
            get;
            private set;
        }

        public Project GetFromID(int projectID)
        {
            foreach (Project p in Projects)
            {
                if (p.ID == projectID)
                    return p;
            }

            return null;
        }

        public void Save()
        {
            foreach (Project p in Projects)
            {
                p.Save();
            }
        }

        internal static ProjectList LoadRecentlyUsed()
        {
            Database db = new Database();
            IEnumerable<ProjectData> data = db.GetMostRecentUsedProjects();

            ProjectList newList = new ProjectList();
            foreach (ProjectData pd in data)
            {
                newList.Projects.Add(Project.CreateFromStore(pd));
            }

            return newList;
        }

        internal static ProjectList Load(bool visibleProjectsOnly)
        {
            return Load(visibleProjectsOnly, false);
        }

        internal static ProjectList Load(bool visibleProjectsOnly, bool excludeLunch)
        {
            Database db = new Database();
            IEnumerable<ProjectData> data = db.GetProjects(visibleProjectsOnly);

            ProjectList newList = new ProjectList();
            foreach (ProjectData pd in data)
            {
                if (!excludeLunch || pd.Code != "LUNCH")
                {
                    newList.Projects.Add(Project.CreateFromStore(pd));
                }
            }

            return newList;
        }
    }
}
