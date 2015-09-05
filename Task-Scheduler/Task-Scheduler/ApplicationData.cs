using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    public class ApplicationData
    {
        static ApplicationData data = new ApplicationData();

        public static ApplicationData Get()
        {
            return data;
        }

        public CategoryStore categoryStore;
        public CalendarItemStore calendarItemStore;

        ApplicationData()
        {
            categoryStore = new CategoryStore();
            calendarItemStore = new CalendarItemStore();
        }

        public void saveAll()
        {
            categoryStore.saveItems();
            calendarItemStore.saveCalenderItems();
        }

        public void LoadAll()
        {
            categoryStore.LoadCalenderItems();
            calendarItemStore.LoadCalenderItems();            
        }

        public void ClearAll()
        {
            calendarItemStore.Clear();
            categoryStore.Clear();
        }

        public void Refresh()
        {
            saveAll();
            ClearAll();
            LoadAll();
        }
    }
}
