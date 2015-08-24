using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task_Scheduler
{
    public enum CalendarItemType
    {
        Event,
        Task
    }

    public class CalendarItem
    {
        public string Name;
        public int id;
        public CalendarItemType Type;

        public string Details;
        public DateTime ItemDate;

        public List<DateTime> WorkAssigned;

        public CalendarItem()
        {
            Name = "";
            id = -1;
            Details = "";
            ItemDate = DateTime.Now;
            WorkAssigned = new List<DateTime>();
        }


        public void FromXml(XmlNode node)
        {
            Name = node.Attributes["Name"].Value;
            Details = node.InnerText;
            id = int.Parse(node.Attributes["Id"].Value);
            ItemDate = DateTime.Parse(node.Attributes["Date"].Value);
        }

        public string ToXML()
        {
            string xml = "<CalendarItem Id=\"" + id.ToString() + "\" Date=\"" + ItemDate.ToString() + "\" Name=\"" + Name + "\" >" + Details + "</CalendarItem>";
            return xml;
        }
    }
}
