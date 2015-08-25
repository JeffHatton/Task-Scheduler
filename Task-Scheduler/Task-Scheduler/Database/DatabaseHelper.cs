using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Task_Scheduler
{
    class DatabaseHelper
    {
        public const string DATABASE_NAME = "Task_App_Database.sqlite";
        public static SQLiteConnection GetDatabaseConnection()
        {
            string databasePath = Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile) + "\\" + DATABASE_NAME;

            if (!File.Exists(databasePath)) createDatabase(databasePath);


            SQLiteConnection connection = new SQLiteConnection("Data Source = " + databasePath + "; Version = 3;");
            connection.Open();
            return connection;
        }

        public static void createDatabase(string databasePath)
        {
            SQLiteConnection.CreateFile(databasePath);

            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source = " + databasePath + "; Version = 3;");
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(DatabaseTables.CalendarItemTableCreation, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                File.Delete(databasePath);
            }
        }
    }
}
