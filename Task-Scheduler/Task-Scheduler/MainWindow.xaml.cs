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
using System.Windows.Forms;

namespace Task_Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationData appData;
        string xmlPath;

        public MainWindow()
        {
            InitializeComponent();
            controlCalender.generateCalenderBoxes();
            appData = ApplicationData.Get();
            appData.LoadAll();

            Registry.SetUpRegistry();
            controlCalender.SetData(ref appData);
            
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            CalenderItemDetailDialog newItemDialog = new CalenderItemDetailDialog();
            newItemDialog.ShowDialog();

            if (newItemDialog.DialogResult.Value)
            {
                CalenderItemDto item = newItemDialog.GUIToObject();
                appData.calendarItemStore.AddItem(item);
            }
        }        

        ~MainWindow()
        {
            appData.saveAll();
        }

        private void Loadbtn_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //DialogResult result = dialog.ShowDialog();

            //// OK button was pressed. 
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    string path = dialog.SelectedPath;
            //    xmlPath = path;
            //    store.LoadCalenderItems(path);
            //}

            //store.LoadCalenderItems();
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            //if (xmlPath == "" || xmlPath == null)
            //{
            //    FolderBrowserDialog dialog = new FolderBrowserDialog();

            //    DialogResult result = dialog.ShowDialog();

            //    // OK button was pressed. 
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        string path = dialog.SelectedPath;
            //        xmlPath = path;
            //    }
            //}

            appData.saveAll();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Controls.TextBox) return;

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Key == Key.N)
                {
                    newButton_Click(null, null);
                }
                if (e.Key == Key.C)
                {
                    menuAddCata_Click(null, null);
                }
            }
        }

        private void menuAddCata_Click(object sender, RoutedEventArgs e)
        {
            CatagoryDetailWindow window = new CatagoryDetailWindow();

            window.ShowDialog();

            if (window.DialogResult.Value)
            {
                CatagoryDto dto = window.GuiToDto();

                appData.categoryStore.AddItem(dto);
            }
        }
    }
}
