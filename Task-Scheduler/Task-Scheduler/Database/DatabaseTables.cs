using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    class DatabaseTables
    {
        public const string CalendarItemTableName = "Calender_Item_Table";

        public const string CalendarItemTableCreation = "CREATE TABLE " + CalendarItemTableName + "(Name TEXT, id INTEGER PRIMARY KEY AUTOINCREMENT, Type TEXT, ItemDate TEXT, Details BLOB);";


        public string Name;
        public int id;
        public CalendarItemType Type;

        public string Details;
        public DateTime ItemDate;
    }
}
