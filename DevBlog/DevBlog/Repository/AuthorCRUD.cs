﻿using Common;

namespace DevBlog.Repository
{
    public class AuthorCRUD
    {
        /// <summary>
        /// Method to create an Author, which also stores it in the current list of objects (local storage) and SQL database (external storage)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Author CreateAuthor(string[] args)
        {
            Author author = new Author();
         
            
            if (args.Length == 5)
            {
                author.ID = int.Parse(args[0]);
                author.Name = args[1];
                author.Email = args[2];
                author.PostCount = int.Parse(args[3]);
                author.Active = bool.Parse(args[4]);
            }
            else if (args.Length == 2)
            {
                author.ID = DatabaseHelper.GetNextID("Author");
                author.Name = args[0];
                author.Email = args[1];
                author.Active = true;
                author.PostCount = 0;
            }
            else
            {
                throw new Exception("Invalid arguments");
            }

            Author.AuthorDB.Add(author);
            Save();
            return author;

        }

        public static void CreateAuthor()
        {
            Console.Clear();
            Author a = new Author();
            Console.WriteLine("Author name: ");
            a.Name = Console.ReadLine();
            Console.WriteLine("Author email: ");
            a.Email = Console.ReadLine();
            a.ID = DatabaseHelper.GetNextID("Author");
            a.Active = true;
            a.PostCount = 0;

            Author.AuthorDB.Add(a);
            Save();
        }
        

        /// <summary>
        /// Saves all Author objects to "external" permanent storage
        /// </summary>
        public static void Save()
        {
            List<string> currentList = DatabaseHelper.GetColumn("Author", "ID");
            foreach (Author author in Author.AuthorDB)
            {
                if (!currentList.Contains(author.ID.ToString()))
                {
                    List<string> fields = new List<string>() { "Name", "Email", "PostCount", "Active" };
                    List<string> values = new List<string>() { author.Name, author.Email, author.PostCount.ToString(), author.Active.ToString() };

                    Database.SaveToDatabase(fields, values, "Author");
                }
            }
        }

        /// <summary>
        /// Loads (async) from persistent database and recreates object in volatile memory
        /// </summary>
        public static async Task LoadAsync()
        {

            List<object> x = await Database.GetAllFromDatabase("Author", false); // False removes writes to console. True by default.

            foreach (List<object> listItem in x)
            {
                string[] vs = listItem.Select(x => x.ToString()).ToArray();
                AuthorCRUD.CreateAuthor(vs);
                
            }

        }

        public static void UpdatePostCount(Author a)
        {
            int newCount = ++Author.AuthorDB.First(x => x.ID == a.ID).PostCount;
            string query =
                "UPDATE Author " +
                $"SET PostCount = {newCount} " +
                $"WHERE ID = {a.ID}";
            Database.QueryDatabase(query);
        }

        /// <summary>
        /// Method for updating an Author object.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="FormatException"></exception>
        public static void Update()
        {
            Console.WriteLine("Please select the Author you wish to edit: ");
            Database.GetAllFromDatabase("Author");
            int id = int.Parse(Console.ReadLine());
            Author target = Author.AuthorDB.Find(x => x.ID == id);
            Console.WriteLine(target.Name);
            Console.WriteLine("Please select the field you wish to edit (name, email, active)");
            string field = Console.ReadLine();

            Console.WriteLine("\nReplace with:");
            string newData = Console.ReadLine();

            Console.WriteLine();
            if (field != null && newData != null && target != null)
            {
                object p = field.ToLower() switch
                {
                    "name" => target.Name = newData,
                    "email" => target.Email = newData,
                    "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
                    _ => throw new FormatException(),
                };
            }

            string query =
                $"UPDATE Author " +
                //$"SELECT * FROM Author WHERE ID = {id} " +
                $"SET {field.UpperFirstChar()} = '{newData}' " +
                $"WHERE ID = {id}";
            Database.QueryDatabase(query);
        }

    }
}
