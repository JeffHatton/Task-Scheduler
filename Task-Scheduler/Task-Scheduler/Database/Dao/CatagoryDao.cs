using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Task_Scheduler
{
    class CatagoryDao
    {
        const string ADD_ITEM_SQL = "insert into " + DatabaseTables.CatagoryName + " (Name, Color) values (@Name,@Color)";
        const string UPDATE_ITEM_SQL = "update " + DatabaseTables.CatagoryName + " set Name = @Name, Color = @Color where id = @id";
        const string SELECT_ALL_ITEMS_SQL = "Select * From " + DatabaseTables.CatagoryName;
        const string SELECT_ITEM_SQL = "";

        public static bool AddItems(List<CatagoryDto> dtos, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            foreach (CatagoryDto dto in dtos)
            {
                AddItem(dto, conn);
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool AddItem(CatagoryDto dto, SQLiteConnection conn = null)
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
                command.Parameters.AddWithValue("@Color", dto.Color.ToString());
                dto.id = command.ExecuteNonQuery();
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool UpdateItems(List<CatagoryDto> dtos, SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            foreach (CatagoryDto dto in dtos)
            {
                UpdateItem(dto, conn);
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool UpdateItem(CatagoryDto dto, SQLiteConnection conn = null)
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
                command.Parameters.AddWithValue("@Color", dto.Color.ToString());
                command.ExecuteNonQuery();
            }

            if (closeCon) conn.Close();
            return false;
        }

        public static bool DeleteCalenderItem(CatagoryDto dto, SQLiteConnection conn = null)
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

        public static Dictionary<int, CatagoryDto> GetAllItems(SQLiteConnection conn = null)
        {
            bool closeCon = false;
            if (conn == null)
            {
                conn = DatabaseHelper.GetDatabaseConnection();
                closeCon = true;
            }

            Dictionary<int, CatagoryDto> dtos = new Dictionary<int, CatagoryDto>();
            using (SQLiteCommand command = new SQLiteCommand(SELECT_ALL_ITEMS_SQL, conn))
            {
                using (SQLiteDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CatagoryDto dto = DtoFromReader(rdr);
                        dtos[dto.id] = dto;
                    }
                }
            }

            if (closeCon) conn.Close();
            return dtos;
        }

        static CatagoryDto DtoFromReader(SQLiteDataReader read)
        {
            CatagoryDto dto = new CatagoryDto();
           
            dto.Name = read["Name"] as string;
            dto.id = Convert.ToInt32(read["id"]);
            dto.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(read["Color"] as string));

            return dto;
        }
    }
}
