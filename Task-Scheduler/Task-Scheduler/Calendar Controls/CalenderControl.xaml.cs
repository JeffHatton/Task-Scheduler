using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_Scheduler
{
    /// <summary>
    /// Interaction logic for CalenderControl.xaml
    /// </summary>
    public partial class CalenderControl : UserControl
    {
        ApplicationData appData;

        Dictionary<DateTime, CalenderBox> MonthlyCalenderBoxes;
        Dictionary<DateTime, CalenderBox> WeeklyCalenderBoxes;
        int month;
        int year;

        public CalenderControl()
        {
            InitializeComponent();

            MonthlyCalenderBoxes = new Dictionary<DateTime, CalenderBox>();
            WeeklyCalenderBoxes = new Dictionary<DateTime, CalenderBox>();
        }

        public void SetData(ref ApplicationData data)
        {
            this.appData = data;

            appData.calendarItemStore.ItemAddedEvt += Store_ItemAddedEvt;
            appData.calendarItemStore.ItemsAddedEvt += Store_ItemsAddedEvt;
        }

        private void Store_ItemsAddedEvt(List<CalenderItemDto> items)
        {
            foreach (CalenderItemDto item in items)
            {
		        AddItemToCalendar(item);
            }
        }

        private void Store_ItemAddedEvt(CalenderItemDto item)
        {
            AddItemToCalendar(item);
        }

	    public void AddItemToCalendar(CalenderItemDto item)
	    {
		    if (MonthlyCalenderBoxes.ContainsKey(item.ItemDate.Date)) MonthlyCalenderBoxes[item.ItemDate.Date].AddItem(item);
		    if (WeeklyCalenderBoxes.ContainsKey(item.ItemDate.Date)) WeeklyCalenderBoxes[item.ItemDate.Date].AddItem(item);
		    if (CalenderBoxDay.ItemDate == item.ItemDate.Date) CalenderBoxDay.AddItem(item);
	    }

        public void generateCalenderBoxes()
        {
            DateTime time = DateTime.Now;

            month = time.Month;
            year = time.Year;          
            DateTime currentDay = new DateTime(year, month, 1);
            DayOfWeek dayOfWeek = currentDay.DayOfWeek;
            currentDay = currentDay.Subtract(new TimeSpan((int)dayOfWeek, 0, 0, 0));

            //
            // Day
            // 
            CalenderBoxDay.setDate(time);           

            //
            // Month
            //
            for (int week = 0; week < 6; week++)
            {
                for (int day = 0; day < 7; day++)
                {                         
                    CalenderBox box = new CalenderBox(currentDay);
                    Grid.SetRow(box, week);
                    Grid.SetColumn(box, day);

                    CalenderGridMonth.Children.Add(box);

                    if (currentDay.CompareTo(DateTime.Now.Date) < 0) box.Background = Brushes.Gray;

                    MonthlyCalenderBoxes[currentDay.Date] = box;
                    currentDay = currentDay.AddDays(1);                    
                }
            }

            //
            // Week
            //
            time = time.Subtract(new TimeSpan((int)time.DayOfWeek, 0, 0, 0));
            for (int day = 0; day < 7; day++)
            {
                CalenderBox box = new CalenderBox(time);
                Grid.SetRow(box, 1);
                Grid.SetColumn(box, day);
                
                CalenderGridWeek.Children.Add(box);
                WeeklyCalenderBoxes[box.ItemDate.Date] = box;
                time = time.AddDays(1);

                if (time.CompareTo(DateTime.Now) < 0) box.Background = Brushes.Gray;
            }
        }

        public void setAllVisible(bool visible)
        {
            foreach (CalenderBox box in MonthlyCalenderBoxes.Values)
            {
                box.setAllItemsVisible(visible);
            }

            foreach (CalenderBox box in WeeklyCalenderBoxes.Values)
            {
                box.setAllItemsVisible(visible);
            }

            CalenderBoxDay.setAllItemsVisible(visible);
        }

        public void FilterAllByField(string fieldName, object value)
        {
            foreach (CalenderBox box in MonthlyCalenderBoxes.Values)
            {
                box.FilterByField(fieldName, value);
            }

            foreach (CalenderBox box in WeeklyCalenderBoxes.Values)
            {
                box.FilterByField(fieldName, value);
            }

            CalenderBoxDay.FilterByField(fieldName, value);
        }
    }
}
