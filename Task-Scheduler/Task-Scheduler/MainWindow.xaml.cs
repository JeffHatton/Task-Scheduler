﻿using System;
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

            store.LoadCalenderItems();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            CalenderItemDetailDialog newItemDialog = new CalenderItemDetailDialog();
            newItemDialog.ShowDialog();

            if (newItemDialog.DialogResult.Value)
            {
                CalenderItemDto item = newItemDialog.GUIToObject();
                store.AddItem(item);
            }
        }        

        ~MainWindow()
        {
            store.saveCalenderItems();
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

            store.LoadCalenderItems();
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

            store.saveCalenderItems();
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            controlCalender.setAllVisible(true);
        }

        private void CatagoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Todo finish
            controlCalender.FilterAllByField("catagoryId", 1);
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            controlCalender.setAllVisible(true);
        }
    }
}
