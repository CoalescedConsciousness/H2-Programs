using System.Data;
using System.Data.SqlClient;
using Common;

namespace Common
{
    public static class Database
    {

        // ConnectionString needed for, well, connecting to the (locally stored) database.
        // Note: If you use a "vs external" database, i.e. an (PostGreSQL) SQL server via pgAdmin, you'll need to use Npgsql.
        internal const string cntStr = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Database = LocalDB";
        
        /// <summary>
        /// Saves data to the SQL database, assigning the list of values to the list of fields, akin to a separated keypair (numerically; x[0]=y[0], x[1]=y[1], etc.)
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="values"></param>
        /// <param name="table"></param>
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
        
        
        public static async Task<List<object>> GetAllFromDatabase(string table, bool console = true, string query = "")
        {
            if (query == "") { query = $"SELECT * FROM {table}"; }; // Refactored to allow for dynamic configuration of query, but still have a standard one available.
            
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
                        DatabaseHelper.GetHeader(reader, console);
                        while (reader.Read())
                        {
                            list.Add(await DatabaseHelper.GetRows((IDataRecord)reader, console));
                        }
                    }
                    
                }
                finally { db.Close(); }
            }
            return list;
            
        }

       
       
    }
}


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