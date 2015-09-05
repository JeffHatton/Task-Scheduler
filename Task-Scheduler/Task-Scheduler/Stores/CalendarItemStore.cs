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
        public Dictionary<int, CalenderItemDto> CalendarItems = new Dictionary<int, CalenderItemDto>();
        public Dictionary<DateTime, List<CalenderItemDto>> CalendarItemsByDate = new Dictionary<DateTime, List<CalenderItemDto>>();
        Dictionary<int, CalenderItemDto> modifiedItems = new Dictionary<int, CalenderItemDto>();
        int currentId = -1;

        public delegate void ItemAddedDel(CalenderItemDto item);
        public delegate void ItemsAddedDel(List<CalenderItemDto> items);
        public delegate void ItemsUpdatedDel(Dictionary<DateTime, List<CalenderItemDto>> updatedItems, Dictionary<DateTime, List<CalenderItemDto>> oldItems);

        public event ItemAddedDel ItemAddedEvt;
        public event ItemsAddedDel ItemsAddedEvt;
        public event ItemsUpdatedDel ItemsUpdatedEvt;

        public CalendarItemStore()
        {

        }

        public void Clear()
        {
            CalendarItems.Clear();
            CalendarItemsByDate.Clear();
            modifiedItems.Clear();
        }

        public void saveCalenderItems()
        {           
            CalenderItemDao.UpdateCalenderItems(modifiedItems.Values.ToList());
            modifiedItems.Clear();
        }

        public void LoadCalenderItems()
        {
            //TODO implement
            CalendarItems = CalenderItemDao.GetAllItems();
            foreach (CalenderItemDto dto in CalendarItems.Values)
            {
                addItemByDate(dto);
            }

            if (ItemsAddedEvt != null) ItemsAddedEvt(CalendarItems.Values.ToList());
        }

        public void AddItem(CalenderItemDto item)
        {
            if (item.id == -1)
            {
                item.id = currentId--;
                CalenderItemDao.AddCalenderItem(item);
            }
            else if (item.id <= currentId) currentId = item.id - 1;
            CalendarItems[item.id] = item;
            addItemByDate(item);
            if (ItemAddedEvt != null) ItemAddedEvt(item);
        }

        public void UpdateItem(CalenderItemDto item)
        {
            if (item.id < 0) return;

            CalenderItemDto oldDto = CalendarItems[item.id];
            CalendarItemsByDate[oldDto.ItemDate.Date].Remove(oldDto);
            addItemByDate(item);
            modifiedItems[item.id] = item;
            CalendarItems[item.id] = item;

            Dictionary<DateTime, List<CalenderItemDto>> updatedItems = new Dictionary<DateTime, List<CalenderItemDto>>();
            updatedItems[item.ItemDate.Date] = new List<CalenderItemDto>();
            updatedItems[item.ItemDate.Date].Add(item);

            Dictionary<DateTime, List<CalenderItemDto>> oldItems = new Dictionary<DateTime, List<CalenderItemDto>>();
            oldItems[oldDto.ItemDate.Date] = new List<CalenderItemDto>();
            oldItems[oldDto.ItemDate.Date].Add(oldDto);

            if (ItemsUpdatedEvt != null) ItemsUpdatedEvt(updatedItems, oldItems);
        }

        private void addItemByDate(CalenderItemDto dto)
        {
            if (!CalendarItemsByDate.ContainsKey(dto.ItemDate.Date)) CalendarItemsByDate[dto.ItemDate.Date] = new List<CalenderItemDto>();
            CalendarItemsByDate[dto.ItemDate.Date].Add(dto);
        }

        public List<CalenderItemDto> getByDate(DateTime date)
        {
            if (CalendarItemsByDate.ContainsKey(date)) return CalendarItemsByDate[date];
            else return new List<CalenderItemDto>();
        }
    }
}
