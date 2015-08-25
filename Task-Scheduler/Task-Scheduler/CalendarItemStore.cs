using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task_Scheduler
{
    public class CalendarItemStore
    {
        Dictionary<int, CalenderItemDto> CalendarItems = new Dictionary<int, CalenderItemDto>();
        Dictionary<int, CalenderItemDto> modifiedItems = new Dictionary<int, CalenderItemDto>();
        Dictionary<int, CalenderItemDto> newItems = new Dictionary<int, CalenderItemDto>();
        int currentId = 0;

        public delegate void ItemAddedDel(CalenderItemDto item);
        public delegate void ItemsAddedDel(List<CalenderItemDto> items);

        public event ItemAddedDel ItemAddedEvt;
        public event ItemsAddedDel ItemsAddedEvt;

        public CalendarItemStore()
        {

        }

        public void saveCalenderItems()
        {
            CalenderItemDao.AddCalenderItems(newItems.Values.ToList());
            CalenderItemDao.UpdateCalenderItems(modifiedItems.Values.ToList());           
        }

        public void LoadCalenderItems()
        {
            //TODO implement
            CalendarItems = CalenderItemDao.GetAllItems();
            ItemsAddedEvt(CalendarItems.Values.ToList());
        }

        public void AddItem(CalenderItemDto item)
        {
            if (item.id == -1)
            {
                item.id = currentId--;
                newItems[item.id] = item;
            }
            else if (item.id <= currentId) currentId = item.id - 1;
            CalendarItems[item.id] = item;
            ItemAddedEvt(item);
        }

        public void UpdateItem(CalenderItemDto item)
        {
            if (item.id < 0) return;
            modifiedItems[item.id] = item;
        }
    }
}
