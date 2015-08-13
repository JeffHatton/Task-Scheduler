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
    /// Interaction logic for CalenderItem.xaml
    /// </summary>
    public partial class CalenderItem : UserControl
    {
        string taskName;

        public CalenderItem(string taskName)
        {
            InitializeComponent();

            this.taskName = taskName;
            lblTaskName.Content = taskName;
        }
    }
}
