using Common;
using DevBlog.Repository;

namespace DevBlog
{
    public class TestMenu
    {
        
        /// <summary>
        /// Method for initating Test menu (async).
        /// </summary>
        internal static async Task RunAsync()
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
                
                MenuHelper.SelectionAsync choiceAsync; // Delegate instantiated
                MenuHelper.Selection choice;

               
                MenuHelper.GetSelection(input, "b", choice = ClearDatabase);
                MenuHelper.GetSelection(input, "r", choice = SelectTable);
                
                MenuHelper.GetSelectionAsync(input, "c", choiceAsync = CreateTestData);

                // If "s", exit while loop.
                runMenu = input.ToLower() == "s" ? false : true;
            }
        }

        // Various selections, note it is the GetSelection 
        private static void SelectTable()
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
        public static void ClearDatabase()
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
            List<Task> tasks = new List<Task>();
            Console.WriteLine("Creating Author(s)");
            Task a = TestAuthorCreate();

            Console.WriteLine("Creating Post(s)");
            Task b = TestPostCreate();

            tasks.Add(a);
            tasks.Add(b);

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Test data created.");
            Console.ReadLine();
        }
        private static async Task TestAuthorCreate()
        {
            _ = AuthorCRUD.CreateAuthor(new string[] { "Mads Madsen", "Testhest@haster.ko" });
            _ = AuthorCRUD.CreateAuthor(new string[] { "Jens Jensen", "Existential@crisis.oh"});
            _ = AuthorCRUD.CreateAuthor(new string[] { "Anders Andersen", "blackjack_and_hookers@own_casino.bender" });
            Console.WriteLine("Created Authors");
        }

        private static async Task TestPostCreate()
        {
            Author a = Author.GetAuthorByName("Mads Madsen");
            PostCRUD.CreatePost(new string[] { a.ID.ToString(), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Test Title" });
            PostCRUD.CreatePost(new string[] { a.ID.ToString(), "Lisa needs braces", "Dental plan" });
            Author b = Author.GetAuthorByName("Jens Jensen");
            PostCRUD.CreatePost(new string[] { b.ID.ToString(), "Something something", "volatile" });
        }
    }
}
