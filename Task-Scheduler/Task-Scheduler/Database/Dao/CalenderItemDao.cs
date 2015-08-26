using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    public class CalenderItemDao
    {
        const string ADD_ITEM_SQL = "insert into " + DatabaseTables.CalendarItemTableName + " (Name, Type, Date, Time, Details, Complete, CategoryId) values (@Name,@Type,@Date,@Time, @Details, @Complete, @CategoryId)";
        const string UPDATE_ITEM_SQL = "update " + DatabaseTables.CalendarItemTableName + " set Name = @Name, Type = @Type, Date = @Date, Time=@Time, Complete = @Complete, Details = @Details, CategoryId=@CategoryId where id = @id";
        const string SELECT_ALL_ITEMS_SQL = "Select * From " + DatabaseTables.CalendarItemTableName;
        const string SELECT_ALL_NONE_COMPLETE_ITEMS_SQL = "Select * From " + DatabaseTables.CalendarItemTableName + " where Complete=0";
        const string SELECT_ITEM_SQL = "";


        public static bool AddCalenderItems(List<CalenderItemDto> dtos, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            foreach (CalenderItemDto dto in dtos)
            {
                AddCalenderItem(dto, conn);
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool AddCalenderItem(CalenderItemDto dto, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            using (SQLiteCommand command = new SQLiteCommand(conn))
            {
                command.CommandText = ADD_ITEM_SQL;
                command.Prepare();
                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@Type", dto.Type.ToString());
                command.Parameters.AddWithValue("@Date", dto.ItemDate.Date.ToString());
                command.Parameters.AddWithValue("@Time", dto.ItemDate.TimeOfDay.ToString());
                command.Parameters.AddWithValue("@Complete", dto.done);
                command.Parameters.AddWithValue("@Details", dto.Details);
                command.Parameters.AddWithValue("@CategoryId", dto.categoryId);                

                dto.id = command.ExecuteNonQuery();
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool UpdateCalenderItems(List<CalenderItemDto> dtos, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            foreach (CalenderItemDto dto in dtos)
            {
                UpdateCalenderItem(dto, conn);
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool UpdateCalenderItem(CalenderItemDto dto, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            using (SQLiteCommand command = new SQLiteCommand(conn))
            {
                command.CommandText = UPDATE_ITEM_SQL;
                command.Prepare();
                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@Type", dto.Type.ToString());
                command.Parameters.AddWithValue("@Date", dto.ItemDate.Date.ToString());
                command.Parameters.AddWithValue("@Time", dto.ItemDate.TimeOfDay.ToString());
                command.Parameters.AddWithValue("@Complete", dto.done);
                command.Parameters.AddWithValue("@Details", dto.Details);
                command.Parameters.AddWithValue("@CategoryId", dto.categoryId);
                command.Parameters.AddWithValue("@id", dto.id);
                command.ExecuteNonQuery();
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool DeleteCalenderItem(CalenderItemDto dto, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static Dictionary<int, CalenderItemDto> GetAllItems(bool GetCompleteItems = false, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            Dictionary<int, CalenderItemDto> dtos = new Dictionary<int, CalenderItemDto>();
            using (SQLiteCommand command = new SQLiteCommand(GetCompleteItems ? SELECT_ALL_ITEMS_SQL : SELECT_ALL_NONE_COMPLETE_ITEMS_SQL, conn))
            {
                using (SQLiteDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CalenderItemDto dto = DtoFromReader(rdr);
                        dtos[dto.id] = dto;
                    }
                }
            }

            if (closeCon) conn.Close();
            return dtos;
        }

        static CalenderItemDto DtoFromReader(SQLiteDataReader read)
        {
            CalenderItemDto dto = new CalenderItemDto();

            dto.Details = read["Details"] as string;
            dto.Name = read["Name"] as string;
            DateTime date = DateTime.Parse(read["Date"] as string);
            DateTime time = DateTime.Parse(read["Time"] as string);
            dto.ItemDate = date.Add(time.TimeOfDay);
            dto.Type = (CalendarItemType)Enum.Parse(typeof(CalendarItemType), read["Type"] as string);
            dto.id = Convert.ToInt32(read["id"]);
            dto.done = (read["Complete"] as bool?).Value;            
            dto.categoryId = Convert.ToInt32(read["CategoryId"]);

            return dto;
        }
    }
}
