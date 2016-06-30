using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RES_Timekeeper.Data
{
    public class ItemList
    {
        public BindingList<Item> Items { get; }

        public ItemList(IList<Item> items)
        {
            Items = new BindingList<Item>(items);
        }

        public ItemList() : this(new List<Item>())
        {
        }

        public int UnconfirmedCount => Items.Where(i => !i.Confirmed).Count();

        public int TotalTimeOverlapSeconds
        {
            get
            {
                int overlap = 0;
                for (int i = 1; i < Items.Count; i++)
                {
                    double diff = (Items[i - 1].StartTime - Items[i - 1].EndTime).TotalSeconds;
                    // Ignore small overlaps
                    if (diff < -1.5)
                    {
                        overlap += overlap;
                    }
                }
                return overlap;
            }
        }

        public void RoundTimesToTheMinute()
        {
            foreach (var item in Items)
            {
                item.StartTime = item.StartTime.Date + new TimeSpan(item.StartTime.Hour, item.StartTime.Minute, 0);
                item.EndTime = item.EndTime.Date + new TimeSpan(item.EndTime.Hour, item.EndTime.Minute, 0);
            }
        }

        internal static ItemList Load(DateTime periodStart, DateTime periodEnd)
        {
            var service = new DataService();
            var data = service.GetItems(periodStart, periodEnd);
            return CreateListFromData(data);
        }

        internal static ItemList Load(bool unconfirmedItemsOnly)
        {
            var service = new DataService();
            var data = service.GetItems(unconfirmedItemsOnly);
            return CreateListFromData(data);
        }

        private static ItemList CreateListFromData(IEnumerable<ItemData> data)
        {
            var items = data.Select(d => Item.CreateFromStore(d)).ToList();
            return new ItemList(items);
        }
    }
}
