using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Task_Scheduler
{
    public class CatagoryDto
    {
        public int id;
        public string Name {get; set; }
        public Color Color { get; set; }

        public CatagoryDto()
        {
            id = -1;
            Name = "";
            Color = Colors.Gray;
        }
    }
}
