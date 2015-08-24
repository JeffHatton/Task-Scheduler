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
using System.Windows.Shapes;

namespace Task_Scheduler
{
    /// <summary>
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItem : Window
    {

        public NewItem()
        {
            InitializeComponent();
        }

        private void btkOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btkCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public CalendarItem GUIToObject()
        {
            CalendarItem item = new CalendarItem();
            item.Details = Details.Text;
            item.ItemDate = date.SelectedDate.Value;
            item.Name = name.Text;

            return item;
        }
    }
}
