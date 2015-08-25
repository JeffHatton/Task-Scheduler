using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler.Database
{
    class DatabaseTables
    {
        public const string CalendarItemTableName = "Calender_Item_Table";

        public const string CalendarItemTableCreation = "CREATE TABLE " + CalendarItemTableName + "(Name TEXT, id INT, Type TEXT, ItemDate TEXT, Details BLOB);";


        public string Name;
        public int id;
        public CalendarItemType Type;

        public string Details;
        public DateTime ItemDate;
    }
}
