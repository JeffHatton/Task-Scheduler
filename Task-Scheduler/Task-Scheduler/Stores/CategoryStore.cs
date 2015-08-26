﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    public class CategoryStore
    {
        public Dictionary<int, CatagoryDto> CalendarItems = new Dictionary<int, CatagoryDto>();
        Dictionary<int, CatagoryDto> modifiedItems = new Dictionary<int, CatagoryDto>();
        Dictionary<int, CatagoryDto> newItems = new Dictionary<int, CatagoryDto>();
        int currentId = 0;

        public delegate void ItemAddedDel(CatagoryDto item);
        public delegate void ItemsAddedDel(List<CatagoryDto> items);

        public event ItemAddedDel ItemAddedEvt;
        public event ItemsAddedDel ItemsAddedEvt;

        public CategoryStore()
        {

        }

        public void saveItems()
        {
            CatagoryDao.AddItems(newItems.Values.ToList());
            CatagoryDao.UpdateItems(modifiedItems.Values.ToList());
        }

        public void LoadCalenderItems()
        {
            //TODO implement
            CalendarItems = CatagoryDao.GetAllItems();
            if (ItemsAddedEvt != null) ItemsAddedEvt(CalendarItems.Values.ToList());
        }

        public void AddItem(CatagoryDto item)
        {
            if (item.id == -1)
            {
                item.id = currentId--;
                newItems[item.id] = item;
            }
            else if (item.id <= currentId) currentId = item.id - 1;
            CalendarItems[item.id] = item;
            if (ItemAddedEvt != null)  ItemAddedEvt(item);
        }

        public void UpdateItem(CatagoryDto item)
        {
            if (item.id < 0) return;
            modifiedItems[item.id] = item;
        }

    }
}
