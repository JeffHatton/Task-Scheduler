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
    /// Interaction logic for CalenderBox.xaml
    /// </summary>
    public partial class CalenderBox : UserControl
    {
        public DateTime ItemDate;
        Dictionary<int, CalenderItemControl> calenderItems = new Dictionary<int, CalenderItemControl>();

        public CalenderBox()
        {
            InitializeComponent();
        }

        public CalenderBox(DateTime date)
        {
            InitializeComponent();

            setDate(date);

            addCalenderItemsToBox();
        }

        public void addCalenderItemsToBox()
        {
            calenderItemsContainer.Children.Clear();

            foreach ( CalenderItemControl item in calenderItems.Values)
            {
                calenderItemsContainer.Children.Add(item);
            }
        }

        public void setDate(DateTime date)
        {
            this.ItemDate = date.Date;
            lblDate.Content = date.Day;
        }

        public void AddItem(CalenderItemDto item)
        {
            calenderItems[item.id] = new CalenderItemControl(item);

            addCalenderItemsToBox();
        }

        public void setAllItemsVisible(bool visible)
        {
            foreach (CalenderItemControl control in calenderItems.Values)
            {
                control.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        

        public void FilterByField(string fieldName, object value)
        {
            foreach (CalenderItemControl control in calenderItems.Values)
            {
                control.FilterByField(fieldName, value);
            }
        }
    }
}
