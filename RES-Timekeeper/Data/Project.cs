using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RES_Timekeeper.Base;

namespace RES_Timekeeper.Data
{
    public class Project : IDatabaseObject
    {
        private bool _isDeleted = false;
        private int _projectID;
        private string _projectCode;
        private string _projectTitle;
        private bool _projectVisible;

        public const int LUNCH_PROJECT_ID = 1;
        public const string LUNCH_PROJECT_NAME = "LUNCH";

        public Project()
        {
            IsNew = true;
            IsDirty = true;

            ID = -1;
            Code = string.Empty;
            Title = string.Empty;
            Visible = true;
        }

        public static Project CreateFromStore(ProjectData data)
        {
            Project loadedProject = new Project();
            loadedProject.ID = data.ID;
            loadedProject.Code = data.Code.Trim();
            loadedProject.Title = data.Title.Trim();
            loadedProject.Visible = data.Visible;
            loadedProject.IsNew = false;
            loadedProject.IsDirty = false;
            return loadedProject;
        }

        public void MarkDeleted()
        {
            Visible = false;
            IsDeleted = true;
        }

        public int ID
        {
            get
            {
                return _projectID;
            }
            private set
            {
                _projectID = value;
            }
        }

        public string Code
        {
            get
            {
                return _projectCode;
            }
            set
            {
                _projectCode = value;
                IsDirty = true;
            }
        }

        public string Title
        {
            get
            {
                return _projectTitle;
            }
            set
            {
                _projectTitle = value;
                IsDirty = true;
            }
        }

        public bool Visible
        {
            get
            {
                return _projectVisible;
            }
            set
            {
                _projectVisible = value;
                IsDirty = true;
            }
        }
        
        
        public bool IsNew
        {
            get;
            private set;
        }

        public bool IsDirty
        {
            get;
            private set;
        }

        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            private set
            {
                IsDirty = true;
                _isDeleted = true;
            }
        }

        public void Save()
        {
            if (IsDirty)
            {
                Database db = new Database();
                if (IsNew)
                {
                    db.InsertProject(Code, Title);
                    IsNew = false;
                }
                else if (IsDeleted)
                {
                    db.DeleteProject(ID);
                }
                else
                {
                    db.UpdateProject(ID, Code, Title, Visible);
                }

                IsDirty = false;
            }
        }
    }
}
