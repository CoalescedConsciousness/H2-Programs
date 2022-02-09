using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Common
{
    public static class Database
    {
        internal const string cntStr = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Database = LocalDB";
        
        //public static void SaveObjectToDB(Object obj, string table)
        //{
        //    using (SqlConnection db = new SqlConnection(cntStr))
        //    {
        //        db.Open();
        //        try
        //        {
        //            string createTable = $@"CREATE TABLE [{table}] (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, [{table}Obj] xml)";
        //            SqlCommand cmd = new SqlCommand(createTable, db);

        //            // Object -> XML
        //            string xmlData = ConvertToXML(obj);
        //            string insert = $@"INSERT INTO [{table}] ([{table}Obj]) VALUES (N'@Obj')";

        //            // Insert
        //            SqlCommand insertionCmd = new SqlCommand(insert, db);
        //            SqlParameter param = insertionCmd.Parameters.AddWithValue("@Obj", xmlData);
        //            param.DbType = DbType.Xml;
        //            insertionCmd.ExecuteNonQuery();
        //        }
        //        finally
        //        { db.Close(); }
        //    }

        //}

        //private static string ConvertToXML(Object obj)
        //{
        //    string xml;
            
        //    XmlSerializer serial = new XmlSerializer(obj.GetType());
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        serial.Serialize(ms, obj);
        //        ms.Position = 0;
        //        xml = new StreamReader(ms).ReadToEnd(); 
        //    }
        //    return xml;
        //}

        //public static Object GetObjectFromDB(int id, string table)
        //{
        //    Object obj = null;
        //    using (SqlConnection db = new SqlConnection(cntStr))
        //    {
        //        db.Open();
        //        string select = $@"SELECT [{table}Obj] FROM [{table}] WHERE ID = {id}";

        //        // Read
        //        SqlCommand selectCmd = new SqlCommand(select, db);
        //        SqlDataReader reader = selectCmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            string xml = reader[0].ToString();
        //            obj = (Object)ConvertXML<Object>(xml);
        //        }
        //    }
        //    return obj;
        //}

        //private static Type ConvertXML<Type>(string xmlString)
        //{
        //    Type obj;

        //    XmlSerializer xml = new XmlSerializer(typeof(Type));
        //    using (StringReader sr = new StringReader(xmlString))
        //    {
        //        obj = (Type)xml.Deserialize(sr);
        //    }
        //    return obj;
        //}

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

            string select = $"SELECT * FROM dbo.{table} ";
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
        
        public static async Task<List<object>> GetAllFromDatabase(string table, bool console = true)
        {
            string query =
                $"SELECT * FROM {table}";
            List<object> list = new List<object>();
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
                            list.Add(await GetRow((IDataRecord)reader, console));
                        }
                    }
                    
                }
                finally { db.Close(); }
            }

            //foreach (List<object> listItem in list)
            //{
            //    Console.WriteLine();
            //    foreach (object item in listItem)
            //    {
            //        Console.Write(item);
            //    }
            //}


            return list;
            
        }

        public static void GetTable(string table)
        {

        }

        private static void GetHeader(SqlDataReader data, bool console = true)
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

        private static async Task<List<object>> GetRow(IDataRecord record, bool console = true)
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
