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
    public partial class CalenderItemDetailDialog : Window
    {
        bool _ReadOnly;
        public bool ReadOnly { get { return _ReadOnly; } set { SetReadonly(value); } }

        public CalenderItemDetailDialog()
        {
            InitializeComponent();

            date.SelectedDate = DateTime.Now;
            comboType.ItemsSource = Enum.GetValues(typeof(CalendarItemType));
            comboType.SelectedValue = CalendarItemType.Task;
            name.Focus();
        }

        private void btkOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btkCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public CalenderItemDto GUIToObject()
        {
            CalenderItemDto item = new CalenderItemDto();
            item.Details = Details.Text;
            item.ItemDate = date.SelectedDate.Value; 
            item.Name = name.Text;

            return item;
        }

        public void ApplyDtoToGUI(CalenderItemDto dto, bool editable = false)
        {
            ReadOnly = !editable;
            Details.Text = dto.Details;
            name.Text = dto.Name;
            date.SelectedDate = dto.ItemDate;
        }

        public void SetReadonly(bool Readonly)
        {
            _ReadOnly = Readonly;

            name.IsEnabled = !Readonly;
            Details.IsEnabled = !Readonly;
            date.IsEnabled = !Readonly;
        }
    }
}
