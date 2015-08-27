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

        Point startPoint;  

        public CalenderItemControl(CalenderItemDto item)
        {
            InitializeComponent();

            this.item = item;
            lblTaskName.Content = item.Name;
            lblType.Content = TypeToChar(item.Type);
            Background = new SolidColorBrush(ApplicationData.Get().categoryStore.CalendarItems[item.categoryId].Color);
            doneChk.Checked -= doneChk_Checked;
            doneChk.IsChecked = item.done;
            doneChk.Checked += doneChk_Checked;
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
                case CalendarItemType.DueDate:
                    charType = "D";
                    break;
            }

            return charType;
        }

        private void doneChk_Checked(object sender, RoutedEventArgs e)
        {
            item.done = (sender as CheckBox).IsChecked.Value;

            ApplicationData.Get().calendarItemStore.UpdateItem(item);
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CalenderItemDetailDialog dialog = new CalenderItemDetailDialog();
            dialog.ApplyDtoToGUI(item, true);
            dialog.ShowDialog();

            if (dialog.DialogResult.Value)
            {
                CalenderItemDto dto = dialog.GUIToObject();
                if (!item.Equals(dto))
                {
                    dto.id = item.id;
                    item = dto;
                    ApplicationData.Get().calendarItemStore.UpdateItem(dto);
                }
            }
        }

        public void FilterByField(string fieldName, object fieldValue)
        {
            PropertyInfo info = typeof(CalenderItemDto).GetProperty(fieldName);

            if (info == null) return;

            if (info.GetValue(item, null).Equals(fieldValue)) Visibility = Visibility.Visible;
            else Visibility = Visibility.Collapsed;
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                //// Get the dragged ListViewItem
                //ListView listView = sender as ListView;
                //ListViewItem listViewItem =
                //    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                //// Find the data behind the ListViewItem
                //Contact contact = (Contact)listView.ItemContainerGenerator.
                //    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", item);
                DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
            }
        }
    }
}

