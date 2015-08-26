using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    public enum CalendarItemType
    {
        Event,
        Task,
        DueDate
    }

    public class CalenderItemDto
    {
        public string Name { get; set; }
        public int id { get; set; }
        public CalendarItemType Type { get; set; }

        public string Details { get; set; }
        public DateTime ItemDate { get; set; }

        public List<DateTime> WorkAssigned { get; set; }

        public bool done { get; set; }
        public int categoryId { get; set; }

        public CalenderItemDto()
        {
            Name = "";
            id = -1;
            Details = "";
            ItemDate = DateTime.Now;
            WorkAssigned = new List<DateTime>();
        }


            //public void FromXml(XmlNode node)
            //{
            //    Name = node.Attributes["Name"].Value;
            //    Details = node.InnerText;
            //    id = int.Parse(node.Attributes["Id"].Value);
            //    ItemDate = DateTime.Parse(node.Attributes["Date"].Value);
            //}

            //public string ToXML()
            //{
            //    string xml = "<CalendarItem Id=\"" + id.ToString() + "\" Date=\"" + ItemDate.ToString() + "\" Name=\"" + Name + "\" >" + Details + "</CalendarItem>";
            //    return xml;
            //}
        }
}
