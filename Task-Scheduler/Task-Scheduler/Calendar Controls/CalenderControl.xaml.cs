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
        CalendarItemStore store;

        Dictionary<string, CalenderBox> CalenderBoxes;
        int month;
        int year;

        public CalenderControl()
        {
            InitializeComponent();

            CalenderBoxes = new Dictionary<string, CalenderBox>();
        }

        public void SetStore(ref CalendarItemStore store)
        {
            this.store = store;

            store.ItemAddedEvt += Store_ItemAddedEvt;
            store.ItemsAddedEvt += Store_ItemsAddedEvt;
        }

        private void Store_ItemsAddedEvt(List<CalendarItem> items)
        {
            foreach (CalendarItem item in items)
            {
                CalenderBoxes[item.ItemDate.ToShortDateString()].AddItem(item);
            }
        }

        private void Store_ItemAddedEvt(CalendarItem item)
        {
            CalenderBoxes[item.ItemDate.ToShortDateString()].AddItem(item);
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
            CalenderBoxes[CalenderBoxDay.date.ToShortDateString()] = CalenderBoxDay;

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

                    CalenderBoxes[currentDay.ToShortDateString()] = box;
                    currentDay = currentDay.AddDays(1);
                }
            }

            //
            // Week
            //
            time = time.Subtract(new TimeSpan((int)time.DayOfWeek, 0, 0, 0));
            for (int day = 0; day < 7; day++)
            {
                CalenderBox box = CalenderBoxes[time.ToShortDateString()];
                Grid.SetRow(box, 1);
                Grid.SetColumn(box, day);

                CalenderGridWeek.Children.Add(box);

                time = time.AddDays(1);

                if (time.CompareTo(DateTime.Now) < 0) box.Background = Brushes.Gray;
            }
        }
    }
}
