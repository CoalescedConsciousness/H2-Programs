using Microsoft.Data.SqlClient;
using ContactList.Models;

namespace ContactsProject.Data
{
	public class Repository : IRepository
	{
        private const string CnctString = "Server=(localdb)\\mssqllocaldb;Database=ContactsProjectContext-44f784c8-82cd-47d7-873f-3f2c454a3ec0;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static List<string> QueryTypes = new List<string>()
        {
            "ContactCreate",
            "ContactDelete",
            "ContactWrite",
            "ContactGetAll",
            "GetContactByID",
            "ContactRestore"
        };
        public static void ConnectToDB(string queryType, params KeyValuePair<string, object>[] pairs)
        {
            using (var conn = new SqlConnection(CnctString))
            using (var cmd = new SqlCommand(queryType, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                for (int i = 0; i < pairs.Length; i++) cmd.Parameters.AddWithValue(pairs[i].Key, pairs[i].Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            } // Connection is closed here when the scope is.
        }
        public static void ContactCreate(string Name, string Email, int Phone, bool IsFavourite)
        {
            KeyValuePair<string, object> name = new KeyValuePair<string, object>("name", Name);
            KeyValuePair<string, object> email = new KeyValuePair<string, object>("email", Email);
            KeyValuePair<string, object> phone = new KeyValuePair<string, object>("phone", Phone);
            KeyValuePair<string, object> fav = new KeyValuePair<string, object>("fav", IsFavourite);
            ConnectToDB(QueryTypes[0], name, email, phone, fav);
        }
        public static void ContactDelete(int ID)
        {
            KeyValuePair<string, object> id = new KeyValuePair<string, object>("id", ID);
            ConnectToDB(QueryTypes[1], id);
        }
        public static void ContactRestore(int ID)
        {
            KeyValuePair<string, object> id = new KeyValuePair<string, object>("id", ID);
            ConnectToDB(QueryTypes[5], id);
        }
        public static void ContactWrite(int ID, string Name, string Email, int Phone, bool IsFavourite)
        {
            KeyValuePair<string, object> id = new KeyValuePair<string, object>("id", ID);
            KeyValuePair<string, object> name = new KeyValuePair<string, object>("name", Name);
            KeyValuePair<string, object> email = new KeyValuePair<string, object>("email", Email);
            KeyValuePair<string, object> phone = new KeyValuePair<string, object>("phone", Phone);
            KeyValuePair<string, object> fav = new KeyValuePair<string, object>("fav", IsFavourite);
            ConnectToDB(QueryTypes[2], id, name, email, phone, fav);
        }
        public static async Task<List<Contact>> ContactGetAllAsync()
        {
            List<Contact> contacts = new List<Contact>();
            using (var conn = new SqlConnection(CnctString))
            using (var cmd = new SqlCommand(QueryTypes[3], conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = new Contact();
                    contact.Id = (int)reader["id"];
                    contact.Name = (string)reader["name"];
                    contact.Email = (string)reader["email"];
                    contact.Phone = (int)reader["phone"];
                    contact.IsFavourite = (bool)reader["IsFavourite"];
                    contact.Active = (bool)reader["active"];
                    contact.EditDate = reader["EditDate"] != DBNull.Value ? (DateTime)reader["EditDate"] : null;
                    contact.CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime)reader["CreateDate"] : null;
                    contacts.Add(contact);
                }
               
            }
            return contacts;
        }
        public static Contact GetContactByID(int? Id)
        {
            Contact contact = new Contact();
            KeyValuePair<string, object> id = new KeyValuePair<string, object>("id", Id);
            using (var conn = new SqlConnection(CnctString))
            using (var cmd = new SqlCommand(QueryTypes[4], conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue(id.Key, id.Value);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    contact.Id = (int)reader["id"];
                    contact.Name = (string)reader["name"];
                    contact.Email = (string)reader["email"];
                    contact.Phone = (int)reader["phone"];
                    contact.IsFavourite = (bool)reader["IsFavourite"];
                    contact.Active = (bool)reader["active"];
                    contact.EditDate = reader["EditDate"] != DBNull.Value ? (DateTime)reader["EditDate"] : null;
                    contact.CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime)reader["CreateDate"] : null;
                }
        
                //cmd.ExecuteNonQuery();
            } // Connection is closed here when the scope is.
            return contact;
        }

    }
}
