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
        CalendarItemStore store;
        string xmlPath;

        public MainWindow()
        {
            InitializeComponent();
            controlCalender.generateCalenderBoxes();
            store = new CalendarItemStore();

            Registry.SetUpRegistry();
            controlCalender.SetStore(ref store);

            store.LoadCalenderItems(Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile));
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            NewItem newItemDialog = new NewItem();
            newItemDialog.ShowDialog();

            if (newItemDialog.DialogResult.Value)
            {
                CalendarItem item = newItemDialog.GUIToObject();
                store.AddItem(item);
            }
        }        

        ~MainWindow()
        {
            store.saveCalenderItems(Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile));
        }

        private void Loadbtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            // OK button was pressed. 
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                xmlPath = path;
                store.LoadCalenderItems(path);
            }
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (xmlPath == "" || xmlPath == null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                DialogResult result = dialog.ShowDialog();

                // OK button was pressed. 
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    xmlPath = path;
                }
            }

            store.saveCalenderItems(xmlPath);
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
            }
        }
    }
}
