using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Task_Scheduler
{
    class CatagoryDto
    {
        public int id;
        public string Name;
        public Brush Color;

        public CatagoryDto()
        {
            id = -1;
            Name = "";
            Color = Brushes.Gray;
        }
    }
}
