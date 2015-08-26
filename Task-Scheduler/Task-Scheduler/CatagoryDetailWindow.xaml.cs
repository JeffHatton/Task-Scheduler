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
    /// Interaction logic for CatagoryDetailWindow.xaml
    /// </summary>
    public partial class CatagoryDetailWindow : Window
    {
        bool _ReadOnly;
        public bool ReadOnly { get { return _ReadOnly; } set { SetReadonly(value); } }

        public CatagoryDetailWindow()
        {
            InitializeComponent();

            ColorPicker.SelectedColor = ColorConverter.ConvertFromString("#CCCCCC") as Color?;
            name.Focus();
        }

        public CatagoryDto GuiToDto()
        {
            CatagoryDto dto = new CatagoryDto();
            dto.Name = name.Text;
            dto.Color = new SolidColorBrush(ColorPicker.SelectedColor.Value);

            return dto;
        }

        public void DtoToGui(CatagoryDto dto)
        {
            name.Text = dto.Name;
            //ColorPicker.SelectedColor = dto.Color
        }

        public void SetReadonly(bool Readonly)
        {
            _ReadOnly = Readonly;

            name.IsEnabled = !Readonly;
            ColorPicker.IsEnabled = !Readonly;
        }

        private void btkOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btkCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
