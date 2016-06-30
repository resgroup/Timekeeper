using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RES_Timekeeper.Data
{
    public class ProjectList
    {
        public BindingList<Project> Projects { get; }

        public ProjectList()
        {
            Projects = new BindingList<Project>();
        }

        public ProjectList(IList<Project> projects)
        {
            Projects = new BindingList<Project>(projects);
        }

        public Project GetFromID(int projectID)
        {
            return Projects.Where(p => p.Id == projectID).FirstOrDefault();
        }

        public void Save()
        {
            var list = Projects.ToList();
            list.ForEach(p => p.Save());
        }

        internal static ProjectList LoadRecentlyUsed()
        {
            var db = new DataService();
            var data = db.GetMostRecentUsedProjects();
            var projects = data.Select(p => Project.CreateFromStore(p)).ToList();

            return new ProjectList(projects);
        }

        internal static ProjectList Load(bool visibleProjectsOnly)
        {
            return Load(visibleProjectsOnly, excludeLunch: false);
        }

        internal static ProjectList Load(bool visibleProjectsOnly, bool excludeLunch)
        {
            var db = new DataService();
            var data = db.GetProjects(visibleProjectsOnly);

            var projects = data.Where(d => !excludeLunch || d.Code != "LUNCH")
                    .Select(d => Project.CreateFromStore(d))
                    .ToList();

            return new ProjectList(projects);
        }
    }
}
