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
        CalenderItemDto dto;
        ApplicationData data;
        bool _ReadOnly;
        public bool ReadOnly { get { return _ReadOnly; } set { SetReadonly(value); } }

        public CalenderItemDetailDialog()
        {
            InitializeComponent();

            data = ApplicationData.Get();
            dto = null;

            date.SelectedDate = DateTime.Now;
            comboType.ItemsSource = Enum.GetValues(typeof(CalendarItemType));
            comboType.SelectedValue = CalendarItemType.Task;
            comboCategory.ItemsSource = data.categoryStore.CalendarItems.Values;
            comboCategory.SelectedItem = data.categoryStore.CalendarItems[1];
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
            if (dto == null) dto = new CalenderItemDto();
            dto.Details = Details.Text;
            dto.ItemDate = date.SelectedDate.Value;
            dto.Name = name.Text;
            dto.Type = (comboType.SelectedItem as CalendarItemType?).Value;

            CatagoryDto Categorydto = comboCategory.SelectedItem as CatagoryDto;

            if (Categorydto != null)
            {
                dto.categoryId = Categorydto.id;
            }            

            return dto;
        }

        public void ApplyDtoToGUI(CalenderItemDto dto, bool editable = false)
        {
            ReadOnly = !editable;
            Details.Text = dto.Details;
            name.Text = dto.Name;
            date.SelectedDate = dto.ItemDate;

            dto = new CalenderItemDto(dto);

            if (data.categoryStore.CalendarItems.ContainsKey(dto.categoryId))
            {
                comboCategory.SelectedValue = data.categoryStore.CalendarItems[dto.categoryId];
            }
        }

        public void SetReadonly(bool Readonly)
        {
            _ReadOnly = Readonly;

            name.IsEnabled = !Readonly;
            Details.IsEnabled = !Readonly;
            date.IsEnabled = !Readonly;
            comboCategory.IsEnabled = !Readonly;
            comboType.IsEnabled = !Readonly;
        }
    }
}
