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
            return newList;
        }
    }
}
