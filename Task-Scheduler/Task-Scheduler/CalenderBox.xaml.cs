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
        DateTime date;
        Dictionary<int, CalenderItem> calenderItems;

        public CalenderBox()
        {
            InitializeComponent();
        }

        public CalenderBox(DateTime date)
        {
            InitializeComponent();

            setDate(date);
            calenderItems = new Dictionary<int, CalenderItem>();

            calenderItems[0] = new CalenderItem("Test1");
            calenderItems[1] = new CalenderItem("Test2");
            calenderItems[2] = new CalenderItem("Test3");

            addCalenderItemsToBox();
        }

        public void addCalenderItemsToBox()
        {
            foreach( CalenderItem item in calenderItems.Values)
            {
                calenderItemsContainer.Children.Add(item);
            }
        }

        public void setDate(DateTime date)
        {
            this.date = date;
            lblDate.Content = date.Day;
        }
    }
}
