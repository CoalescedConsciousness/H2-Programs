using Common;
using DevBlog.Repository;

namespace DevBlog
{
    public class TestMenu
    {
        
        /// <summary>
        /// Method for initating Test menu (async).
        /// </summary>
        internal static async void RunAsync()
        {
            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Choose test option");
                Console.WriteLine("[B] Clear Database(s)");
                Console.WriteLine("[R] Read all data from (a) table");
                Console.WriteLine("[C] Create test data");
                Console.WriteLine("[S] Skip");

                string input = Console.ReadLine();
                
                MenuHelper.SelectionAsync choice; // Delegate instantiated

               
                await MenuHelper.GetSelectionAsync(input, "b", choice = ClearDatabase);
                await MenuHelper.GetSelectionAsync(input, "r", choice = SelectTable);
                await MenuHelper.GetSelectionAsync(input, "c", choice = CreateTestData);

                // If "s", exit while loop.
                runMenu = input.ToLower() == "s" ? false : true;
            }
        }

        // Various selections, note it is the GetSelection 
        private static async Task SelectTable()
        {
            Console.Clear();
            Console.WriteLine("Select Table:");
            Console.WriteLine("[A] Authors");
            Console.WriteLine("[P] Posts");

            string input = Console.ReadLine();
            Console.Clear();
            if (input.ToLower() == "a") { Database.GetAllFromDatabase("Author"); };
            if (input.ToLower() == "p") { Database.GetAllFromDatabase("Post"); };

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
        public static async Task ClearDatabase()
        {
            string query =
                "IF EXISTS(SELECT 1 FROM sys.Tables WHERE Name= N'Author' AND Type = N'U') BEGIN DROP TABLE Author END;" +
                "IF EXISTS(SELECT 1 FROM sys.Tables WHERE Name= N'Post' AND Type = N'U') BEGIN DROP TABLE Post END;" +
                "CREATE TABLE [dbo].Author (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name varchar(50) NULL, Email varchar(50) NULL, PostCount int NULL, Active varchar(5) NOT NULL);" +
                "CREATE TABLE [dbo].Post (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, Title varchar(50) NULL, Body varchar(500) NULL, Author varchar(50) NOT NULL, AuthorID varchar(5) NULL, Links varchar(50) NULL, Active bit NULL);";
            //"INSERT INTO [dbo].Author (Name, Email, PostCount, Active)" +
            //"VALUES ('Test', 'Test@test.dk', '5', 'False');" +
            //"INSERT INTO [dbo].Author (Name, Email, PostCount, Active)" +
            //"VALUES ('Test2', 'Test2@test.dk', '52', 'True');";
            Post.PostDB.Clear();
            Author.AuthorDB.Clear();

            Database.QueryDatabase(query);

        }

        public static async Task CreateTestData()
        {
            
            Author author = TestAuthorCreate();
            Console.WriteLine("Creating Author");
            
            Post post = TestPostCreate(author);
            Console.WriteLine("Creating Test Data");

            Console.WriteLine($"{author.Name} wrote the post '{post.Title}', reading:\n\n{post.Body}");

        }
        private static Author TestAuthorCreate()
        {
            return AuthorCRUD.CreateAuthor(new string[] { "Test", "Testhest@haster.ko" });
        }

        private static Post TestPostCreate(Author author)
        {
            string[] a = new string[] { author.ID.ToString(), "Test", "Tast" };
            Post p = PostCRUD.CreatePost(a);
            return p;
            //return PostCRUD.CreatePost(new string[] { author.ID.ToString(), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Test Title" });
            
        }
    }
}
