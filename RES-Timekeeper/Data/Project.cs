using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Data
{
    public class Project
    {
        private bool _isDeleted = false;

        public const int LUNCH_PROJECT_ID = 1;
        public const string LUNCH_PROJECT_NAME = "LUNCH";

        public int Id { get; }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                IsDirty = true;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                IsDirty = true;
            }
        }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                IsDirty = true;
            }
        }
        public bool Archived
        {
            get { return !Visible; }
            set
            {
                Visible = !value;
            }
        }

        public bool IsNew { get; private set; }

        public bool IsDirty { get; private set; }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            private set
            {
                IsDirty = true;
                _isDeleted = true;
            }
        }
        public ProjectData Data => new ProjectData(Id, Code, Title, Visible);

        public Project() : this(-1) { }
        public Project(int id)
        {
            this.Id = id;
            IsNew = true;
            IsDirty = true;

            Code = string.Empty;
            Title = string.Empty;
            Visible = true;
        }

        public static Project CreateFromStore(ProjectData data)
        {
            return new Project(data.ID)
            {
                Code = data.Code.Trim(),
                Title = data.Title.Trim(),
                Visible = data.Visible,
                IsNew = false,
                IsDirty = false
            };
        }

        public void MarkDeleted()
        {
            Visible = false;
            IsDeleted = true;
        }


        public void Save()
        {
            if (IsDirty)
            {
                var database = new DataService();
                if (IsNew)
                {
                    database.InsertProject(Code, Title);
                    IsNew = false;
                }
                else if (IsDeleted)
                {
                    database.DeleteProject(Id);
                }
                else
                {
                    database.UpdateProject(this.Data);
                }
                IsDirty = false;
            }
        }
    }
}
