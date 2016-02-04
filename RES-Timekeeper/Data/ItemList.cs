using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RES_Timekeeper.Base;
using System.ComponentModel;

namespace RES_Timekeeper.Data
{
    public class ItemList
    {
        public ItemList()
        {
            Items = new BindingList<Item>();
        }

        public BindingList<Item> Items
        {
            get;
            private set;
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public int UnconfirmedCount
        {
            get
            {
                return Items.Where(i => !i.Confirmed).Count();
            }
        }

        public int TotalTimeOverlapSeconds
        {
            get
            {
                int overlap = 0;
                for (int i = 1; i < Count; i++)
                {
                    double diff = (Items[i - 1].StartTime - Items[i - 1].EndTime).TotalSeconds;
                    if (diff < -1.5)    // Ignore small overlaps
                        overlap += (int)overlap;
                }
                return overlap;
            }
        }

        public void RoundTimesToTheMinute()
        {
            foreach (var i in Items)
            {
                i.StartTime = i.StartTime.Date + new TimeSpan(i.StartTime.Hour, i.StartTime.Minute, 0);
                i.EndTime = i.EndTime.Date + new TimeSpan(i.EndTime.Hour, i.EndTime.Minute, 0);
            }
        }

        internal static ItemList Load(DateTime periodStart, DateTime periodEnd)
        {
            Database db = new Database();
            IEnumerable<ItemData> data = db.GetItems(periodStart, periodEnd);
            return CreateListFromData(data);
        }

        internal static ItemList Load(bool unconfirmedItemsOnly)
        {
            Database db = new Database();
            IEnumerable<ItemData> data = db.GetItems(unconfirmedItemsOnly);
            return CreateListFromData(data);
        }

        private static ItemList CreateListFromData(IEnumerable<ItemData> data)
        {
            ItemList newList = new ItemList();
            foreach (ItemData id in data)
            {
                newList.Items.Add(Item.CreateFromStore(id));
            }

            // Need to sanitize the data to ensure overlaps in the database don't make it back to the UI
            // Known overlaps have occured when people have run multiple copies by accident
            /*int i = 0;
            while (i < newList.Count-1)
            {
                if (newList.Items[i].StartTime < newList.Items[i+1].EndTime)
                {
                    Item replacement = Item.CreateNewItem(newList.Items[i+1].StartTime, newList.Items[i].StartTime);
                    replacement.Confirmed = newList.Items[i+1].Confirmed;
                    replacement.Notes = newList.Items[i+1].Notes;
                    replacement.ProjectID = newList.Items[i+1].ProjectID;

                    newList.Items[i+1] = replacement;
                    if (newList.Items[i+1].EndTime <= newList.Items[i+1].StartTime)
                    {
                        newList.Items.RemoveAt(i+1);
                    }
                }
                i++;
            }*/

            return newList;
        }
    }
}
