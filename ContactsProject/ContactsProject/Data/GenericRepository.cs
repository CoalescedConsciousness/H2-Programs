using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ContactList.Models;
using System.Reflection;

namespace ContactsProject.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private ContactsProjectContext _context = null;
        private DbSet<T> table = null;
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

        public static void ConnectToDB(string queryType, List<KeyValuePair<string, object>> pairs)
        {
            using (var conn = new SqlConnection(CnctString))
            using (var cmd = new SqlCommand(queryType, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                for (int i = 0; i < pairs.Count; i++) cmd.Parameters.AddWithValue(pairs[i].Key, pairs[i].Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            } // Connection is closed here when the scope is.
        }

        public GenericRepository(ContactsProjectContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            //return table.ToList();
            List<T> objects = new List<T>();
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
                    T thisObj = new T();
                    //thisObj.Id = (int)reader["id"];
                    //thisObj.Name = (string)reader["name"];
                    //thjis.Email = (string)reader["email"];
                    //contact.Phone = (int)reader["phone"];
                    //contact.IsFavourite = (bool)reader["IsFavourite"];
                    //contact.Active = (bool)reader["active"];
                    //contact.EditDate = reader["EditDate"] != DBNull.Value ? (DateTime)reader["EditDate"] : null;
                    //contact.CreateDate = reader["CreateDate"] != DBNull.Value ? (DateTime)reader["CreateDate"] : null;
                    //contacts.Add(contact);
                    Type t = thisObj.GetType();
                    var prop = t.GetProperties();
                    for (int i = 0; i < prop.Length; i++)
                    {
                        var val = prop.GetValue(i);
                        prop.SetValue(val, i);
                        
                    }
                    Type construct = t.MakeGenericType(prop);
                }

            }
            return objects;
        }

        public T GetByID(object id)
        {
            return table.Find(id);
        }
        public List<KeyValuePair<string, object>> GetFields(object o)
        {
            List<KeyValuePair<string, object>> listKP = new List<KeyValuePair<string, object>>();
            Type t = o.GetType();
            var p = t.GetProperties();
            foreach (PropertyInfo pi in p)
            {
                KeyValuePair<string, object> pair = new KeyValuePair<string, object>(pi.Name, pi.GetValue(o, null));
                listKP.Add(pair);
            }
            return listKP;
        }

        public void Create(T entity)
        {
            List<KeyValuePair<string, object>> listKP = new List<KeyValuePair<string, object>>();
            listKP = GetFields(entity);
            ConnectToDB(QueryTypes[0], listKP);
            //table.Add(entity);
        }

        public void Update(T entity)
        {
            List<KeyValuePair<string, object>> listKP = new List<KeyValuePair<string, object>>();
            listKP = GetFields(entity);
            ConnectToDB(QueryTypes[2], listKP);
            //table.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;

        }

        public void Delete(object idTarget)
        {
            List<KeyValuePair<string, object>> listKP = new List<KeyValuePair<string, object>>();
            listKP.Add(new KeyValuePair<string, object>("id", idTarget));
            ConnectToDB(QueryTypes[1], listKP);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
