using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task_Scheduler
{
    public class CalendarItemStore
    {
        Dictionary<int, CalendarItem> CalendarItems = new Dictionary<int, CalendarItem>();
        int currentId = 0;

        public delegate void ItemAddedDel(CalendarItem item);
        public delegate void ItemsAddedDel(List<CalendarItem> items);

        public event ItemAddedDel ItemAddedEvt;
        public event ItemsAddedDel ItemsAddedEvt;

        public CalendarItemStore()
        {

        }

        public void saveCalenderItems(string xmlPath)
        {
            //TODO implement
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(xmlPath + "\\Tasks.xml", settings);

            XmlDocument doc = new XmlDocument();

            string xml = "<Items>";
            foreach (CalendarItem item in CalendarItems.Values)
            {
                xml += "\n" + item.ToXML();
            }

            xml += "\n</Items>";

            doc.LoadXml(xml);
            doc.Save(writer);
        }

        public void LoadCalenderItems()
        {
            //TODO implement
        }

        public void LoadCalenderItems(string xamlPath)
        {
            //TODO implement
            XmlDocument doc = new XmlDocument();
            string xmlFilePath = xamlPath + "\\Tasks.xml";

            if (File.Exists(xmlFilePath))
            {
                List<CalendarItem> items = new List<CalendarItem>();
                doc.Load(xmlFilePath);                

                foreach (XmlNode node in doc.DocumentElement.SelectNodes("//Items/CalendarItem"))
                {
                    CalendarItem item = new CalendarItem();
                    item.FromXml(node);
                    CalendarItems[item.id] = item;
                    items.Add(item);
                }

                ItemsAddedEvt(items);
            }
        }

        public void AddItem(CalendarItem item)
        {
            if (item.id == -1)
            {
                item.id = currentId++;
            }
            else if (item.id >= currentId) currentId = item.id + 1;
            CalendarItems[item.id] = item;
            ItemAddedEvt(item);
        }
    }
}
