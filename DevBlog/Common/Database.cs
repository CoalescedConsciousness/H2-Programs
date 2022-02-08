using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public static class Database
    {
        internal const string cntStr = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Database = LocalDB";
       
        public static void SaveToDatabase(List<string> fields, List<string> values, string table)
        {
            string sanitizeFields = default;
            string sanitizeValues = default;
            int counter = 0;
            foreach (string field in fields) { sanitizeFields = sanitizeFields + $"{field}, "; }
            foreach (string val in values)
            {
                sanitizeValues = sanitizeValues + $"@val{counter}, ";
                counter++;
            }
            sanitizeFields = sanitizeFields.TrimEnd().TrimEnd(',');
            sanitizeValues = sanitizeValues.TrimEnd().TrimEnd(',');

            string select = $"SELECT * FROM dbo.Author ";
            string query = $"INSERT INTO dbo.{table} ({sanitizeFields}) VALUES ({sanitizeValues})";
            
            
            
            using SqlConnection db = new SqlConnection(cntStr);
            {
                using SqlDataAdapter dataAdapt = new SqlDataAdapter();
                {
                    dataAdapt.SelectCommand = new SqlCommand(select, db);
                    dataAdapt.InsertCommand = new SqlCommand(query, db);
                    for (int i = 0; i < fields.Count; i++) { dataAdapt.InsertCommand.Parameters.AddWithValue($"val{i}", values[i]); };

                    using DataSet set = new DataSet();
                    {
                        dataAdapt.Fill(set);

                        DataRow nRow = set.Tables[0].NewRow();
                        for (int i = 0; i < fields.Count; i++)
                        {
                            nRow[fields[i]] = values[i];
                        }
                        set.Tables[0].Rows.Add(nRow);
                        dataAdapt.Update(set);
                    }
                }
            }
        }

        public static int GetNextID(string table)
        {
            List<string> ids = Database.GetColumn(table);
            List<int> intIds = new List<int>();
            {
                foreach (string id in ids)
                {
                    intIds.Add(int.Parse(id));
                }
            }
           
            return intIds.Count > 0 ? intIds.Max() + 1 : 1;
        }

        public static void QueryDatabase(string query)
        {

            using SqlConnection db = new SqlConnection(cntStr);
            using SqlCommand sqlCmd = new SqlCommand(query, db);
            
            try
            {

                db.Open();
                sqlCmd.Connection = db;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = query;
                sqlCmd.ExecuteNonQuery();
            }

            finally
            {
                db.Close();
            }
        }
        public static List<string> GetColumn(string table, string column="ID")
        {
            List<string> ids = new List<string>();
            string query = $"SELECT {column} FROM {table}";
            using SqlConnection db = new SqlConnection(cntStr);
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
        
        public static Task<List<object>> GetAllFromDatabase(string table, string console = "true")
        {
            string query =
                $"SELECT * FROM {table}";
            Task<List<object>> list = default;
            using (SqlConnection db = new SqlConnection(cntStr))
            using (SqlCommand sqlCmd = new SqlCommand(query, db))
            {
                try
                {
                    db.Open();
                    sqlCmd.Connection = db;
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        Console.WriteLine();
                        GetHeader(reader, console);
                        while (reader.Read())
                        {
                            list = GetRow((IDataRecord)reader, console);
                        }
                    }
                    
                }
                finally { db.Close(); }
            }

            return list;
            
        }

        public static void GetTable(string table)
        {

        }

        private static void GetHeader(SqlDataReader data, string console = "true")
        {
            if (console == "true")
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    Console.Write($"| {data.GetName(i),15}   |");
                }
                Console.WriteLine();
            }
        }

        private static async Task<List<object>> GetRow(IDataRecord record, string console = "true")
        {
            List<object> recList = new List<object>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                recList.Add(record[i]);
                if (console == "true")
                {
                    Console.Write($"| {record[i],15}   |");
                }
            }
            return recList;
            
        }

       
    }
}
