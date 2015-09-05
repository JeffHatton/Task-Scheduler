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

            ApplicationData.Get().calendarItemStore.ItemsUpdatedEvt += CalendarItemStore_ItemsUpdatedEvt;
        }

        private void CalendarItemStore_ItemsUpdatedEvt(Dictionary<DateTime, List<CalenderItemDto>> updatedItems, Dictionary<DateTime, List<CalenderItemDto>> oldItems)
        {
            if (updatedItems.ContainsKey(ItemDate.Date) || oldItems.ContainsKey(ItemDate.Date))
            {
                Refresh();
            }
        }

        public CalenderBox(DateTime date)
        {
            InitializeComponent();
            setDate(date);
            List<CalenderItemDto> items =  ApplicationData.Get().calendarItemStore.getByDate(date.Date);

            foreach (CalenderItemDto dto in items)
            {
                calenderItems[dto.id] = new CalenderItemControl(dto);
            }

            addCalenderItemsToBox();

            ApplicationData.Get().calendarItemStore.ItemsUpdatedEvt += CalendarItemStore_ItemsUpdatedEvt;
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

        public void ClearItems()
        {
            calenderItems.Clear();
            calenderItemsContainer.Children.Clear();
        }

        public void AddItems(List<CalenderItemDto> items)
        {
            foreach (CalenderItemDto dto in items)
            {
                AddItem(dto);
            }
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

        public void Refresh()
        {
            ClearItems();
            AddItems( ApplicationData.Get().calendarItemStore.getByDate(ItemDate.Date));
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                if (e.Data.GetData("myFormat") is CalenderItemDto)
                {
                    CalenderItemDto dto = e.Data.GetData("myFormat") as CalenderItemDto;

                    if (dto.ItemDate.Date != this.ItemDate)
                    {
                        dto = new CalenderItemDto(dto);
                        dto.ItemDate = this.ItemDate;

                        ApplicationData.Get().calendarItemStore.UpdateItem(dto);
                    }
                }
            }
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu.Visibility = Visibility.Visible;
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CalenderItemDetailDialog dialog = new CalenderItemDetailDialog();
            dialog.setDate(ItemDate.Date);

            dialog.ShowDialog();
            if (dialog.DialogResult.Value)
            {
                CalenderItemDto dto = dialog.GUIToObject();
                ApplicationData.Get().calendarItemStore.AddItem(dto);
            }
        }
    }
}
