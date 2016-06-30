using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RES_Timekeeper.Base;

namespace RES_Timekeeper.Data
{
    public class Item : IDatabaseObject, IComparable
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
            Item loadedItem = new Item();
            loadedItem.ProjectID = data.ProjectID;
            loadedItem.StartTime = data.StartTime;
            loadedItem.EndTime = data.EndTime;
            loadedItem._originalStartTime = data.StartTime;
            loadedItem._originalEndTime = data.EndTime;
            loadedItem.Confirmed = data.Confirmed;
            loadedItem.Notes = data.Notes;
            loadedItem.IsNew = false;
            loadedItem.IsDirty = false;
            return loadedItem;
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
            Item i = new Item();
            i.ProjectID = 1;
            i.StartTime = startTime;
            i.EndTime = endTime;
            i.Confirmed = false;
            i.Notes = string.Empty;

            return i;
        }

        public static Item GetMostRecentItem()
        {
            Database db = new Database();
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
                Database db = new Database();
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

        public bool IsDirty
        {
            get;
            private set;
        }

        public bool IsNew
        {
            get;
            private set;
        }

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
            return this.StartTime.CompareTo(((Item)obj).StartTime);
        }
    }
}
