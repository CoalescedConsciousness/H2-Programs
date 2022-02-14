using System.Data.SqlClient;
using System.Data;

namespace Common
{
    /// <summary>
    /// Class that contains helper methods when interacting with the main SQL database interface in Database.cs
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Helper method used to predict the next ID generated for a given tabel, for assignment during object creation (Author, Post).
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetNextID(string table)
        {
            List<string> ids = GetColumn(table);
            List<int> intIds = new List<int>();
            {
                foreach (string id in ids)
                {
                    intIds.Add(int.Parse(id));
                }
            }

            return intIds.Count > 0 ? intIds.Max() + 1 : 1;
        }

        /// <summary>
        /// Simple method used to get the values from a specific column in a specific table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static List<string> GetColumn(string table, string column = "ID")
        {
            List<string> ids = new List<string>();
            string query = $"SELECT {column} FROM {table}";
            using SqlConnection db = new SqlConnection(Database.cntStr);
            using SqlCommand cmd = new SqlCommand(query, db);
            {
                try
                {
                    db.Open();
                    cmd.Connection = db;
                    SqlDataReader result = cmd.ExecuteReader();

                    while (result.Read())
                    {
                        ids.Add(result[column].ToString());
                    }
                }
                finally { db.Close(); }
            }
            return ids;
        }

        /// <summary>
        /// Helper method to get a specific, singular record (row) from a tabel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        public static void GetRecord(string table, int id)
        {
            string query = $"SELECT * FROM {table} WHERE ID = {id}";
            Database.GetAllFromDatabase(table, true, query);
        }

        /// <summary>
        /// Helper method to fetch column names.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="console"></param>
        internal static void GetHeader(SqlDataReader data, bool console = true)
        {
            if (console == true)
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    Console.Write($"| {data.GetName(i),15}   |");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Helper method used to iterate through fetched records (rows) -- notice plural; do not confuse with GetRecord!
        /// </summary>
        /// <param name="record"></param>
        /// <param name="console"></param>
        /// <returns></returns>
        internal static async Task<List<object>> GetRows(IDataRecord record, bool console = true)
        {
            List<object> recList = new List<object>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                recList.Add(record[i]);
                if (console == true)
                {
                    Console.Write($"| {record[i],15}   |");
                }
            }

            Console.WriteLine();
            return recList;

        }

    }
}
