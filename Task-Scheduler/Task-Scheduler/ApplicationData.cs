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
            calendarItemStore.LoadCalenderItems();
            categoryStore.LoadCalenderItems();
        }

    }
}
