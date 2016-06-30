using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Data
{
    public class Item : IComparable
    {
        private bool _isDeleted = false;

        private int _projectID;
        private DateTime _originalStartTime;
        private DateTime _originalEndTime;
        private DateTime _startTime;
        private DateTime _endTime;
        private bool _confirmed;
        private string _notes;


        private Item()
        {
            IsNew = true;
            IsDirty = true;

            _projectID = 0;
            _startTime = DateTime.Now;
            _endTime = _startTime;
            _originalStartTime = _startTime;
            _originalEndTime = _startTime;
            _confirmed = false;
            _notes = string.Empty;
        }

        public static Item CreateFromStore(ItemData data)
        {
            return new Item
            {
                ProjectID = data.ProjectID,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                _originalStartTime = data.StartTime,
                _originalEndTime = data.EndTime,
                Confirmed = data.Confirmed,
                Notes = data.Notes,
                IsNew = false,
                IsDirty = false
            };
        }

        public int ProjectID
        {
            get { return _projectID; }
            set
            {
                _projectID = value;
                IsDirty = true;
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                IsDirty = true;
            }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                IsDirty = true;
            }
        }

        public bool Confirmed
        {
            get { return _confirmed; }
            set
            {
                _confirmed = value;
                IsDirty = true;
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                IsDirty = true;
            }
        }

        public static Item CreateNewItem(DateTime startTime, DateTime endTime)
        {
            return new Item
            {
                ProjectID = 1,
                StartTime = startTime,
                EndTime = endTime,
                Confirmed = false,
                Notes = string.Empty
            };
        }

        public static Item GetMostRecentItem()
        {
            DataService db = new DataService();
            ItemData data = db.GetMostRecentItem();
            if (data == null)
                return null;
            else
                return CreateFromStore(data);
        }

        public void Save()
        {
            if (IsDirty)
            {
                DataService db = new DataService();
                if (IsNew)
                {
                    db.InsertItem(ProjectID, StartTime, EndTime, Confirmed, Notes);
                    _originalStartTime = StartTime;
                    _originalEndTime = EndTime;
                    IsNew = false;
                }
                else if (IsDeleted)
                {
                    db.DeleteItem(StartTime, EndTime);
                }
                else
                {
                    db.UpdateItem(ProjectID, _originalStartTime, _originalEndTime, StartTime, EndTime, Confirmed, Notes);
                    _originalStartTime = StartTime;
                    _originalEndTime = EndTime;
                }

                IsDirty = false;
            }
        }

        public bool IsDirty { get; private set; }

        public bool IsNew { get; private set; }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            private set
            {
                _isDeleted = value;
                IsDirty = true;
            }
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
        }

        public int CompareTo(object obj)
        {
            var item = obj as Item;
            if (item != null)
            {
                return this.StartTime.CompareTo(item.StartTime);
            }
            else
            {
                return -1;
            }
        }
    }
}
