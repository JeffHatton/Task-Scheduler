using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CalenderItem.xaml
    /// </summary>
    public partial class CalenderItemControl : UserControl
    {
        CalenderItemDto item;        

        public CalenderItemControl(CalenderItemDto item)
        {
            InitializeComponent();

            this.item = item;
            lblTaskName.Content = item.Name;
            lblType.Content = TypeToChar(item.Type);
        }

        public string TypeToChar(CalendarItemType type)
        {
            string charType = "";
            switch (type)
            {
                case CalendarItemType.Event:
                    charType = "E";
                    break;
                case CalendarItemType.Task:
                    charType = "T";
                    break;
            }

            return charType;
        }

        private void doneChk_Checked(object sender, RoutedEventArgs e)
        {
            item.done = (sender as CheckBox).IsChecked.Value;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CalenderItemDetailDialog dialog = new CalenderItemDetailDialog();
            dialog.ApplyDtoToGUI(item);
            dialog.ShowDialog();

            if (dialog.DialogResult.Value)
            {
                //CalenderItemDto dto = dialog.GUIToObject();
                //if (item != dto)
                //{
                //    dto.id = item.id;
                //    item = dto;
                                        
                //}
            }
        }

        public void FilterByField(string fieldName, object fieldValue)
        {
            PropertyInfo info = typeof(CalenderItemDto).GetProperty(fieldName);

            if (info == null) return;

            if (info.GetValue(item, null) != fieldValue) Visibility = Visibility.Collapsed;
        }
    }
}

