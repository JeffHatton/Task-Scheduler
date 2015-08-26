﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    class DatabaseTables
    {
        public const string CalendarItemTableName = "Calender_Item_Table";
        public const string CalendarItemTableCreation = "CREATE TABLE " + CalendarItemTableName + "(Name TEXT, id INTEGER PRIMARY KEY AUTOINCREMENT, Type TEXT, Date TEXT, Time TEXT, Details BLOB, Complete Bool, FilePaths TEXT, SubTasks TEXT );";

        public const string CatagoryName = "Catagory_Table";
        public const string CatagoryTableCreation = "CREATE TABLE " + CatagoryName + "(Name TEXT, id INTEGER PRIMARY KEY AUTOINCREMENT, Color TEXT);";
    }
}
