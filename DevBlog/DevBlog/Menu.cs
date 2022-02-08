using Common;

namespace DevBlog
{
    public class Menu
    {
        public delegate bool MenuState(string input, string target);
        public static void Run()
        {
            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Choose test option");
                Console.WriteLine("[B] Repopulate Database");
                Console.WriteLine("[R] Read all data from (a) table");
                Console.WriteLine("[C] Create test data");
                Console.WriteLine("[S] Skip");

                string input = Console.ReadLine();

                Selection choice; // Delegate instantiated
                
                GetSelection(input, "b", choice = RepopulateDatabase);
                GetSelection(input, "r", choice = SelectTable);
                GetSelection(input, "c", choice = CreateTestData);

                // If "s", exit while loop.
                runMenu = input.ToLower() == "s" ? false : true;
            }

            runMenu = true;
            while (runMenu)
            {
                Console.Clear();
            }
        }
        internal delegate void Selection();


        internal static void GetSelection(string input, string target, Selection choice)
        {
            if (input.ToLower() == target.ToLower()) { choice(); return; }
        }



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
        private static void RepopulateDatabase()
        {
            string query =
                "IF EXISTS(SELECT 1 FROM sys.Tables WHERE Name= N'Author' AND Type = N'U') BEGIN DROP TABLE Author END;" +
                "IF EXISTS(SELECT 1 FROM sys.Tables WHERE Name= N'Post' AND Type = N'U') BEGIN DROP TABLE Post END;" +
                "CREATE TABLE [dbo].Author (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name varchar(50) NULL, Email varchar(50) NULL, PostCount int NULL, Active varchar(5) NULL);" +
                "CREATE TABLE [dbo].Post (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, Title varchar(50) NULL, Body varchar(500) NULL);" +
                "INSERT INTO [dbo].Author (Name, Email, PostCount, Active)" +
                "VALUES ('Test', 'Test@test.dk', '5', 'False');" +
                "INSERT INTO [dbo].Author (Name, Email, PostCount, Active)" +
                "VALUES ('Test2', 'Test2@test.dk', '52', 'True');";

            Database.QueryDatabase(query);

        }

        public static async void CreateTestData()
        {
            //Task<Author> author = TestAuthorCreate();
            //Post post = await TestPostCreate(await author);
            Author author = TestAuthorCreate();
            Post post = TestPostCreate(author);
            Console.WriteLine("Creating Test Data");
            //await post;
            Console.WriteLine($"{author.Name} wrote the post '{post.Title}', readin:\n\n{post.Body}");

        }
        private static Author TestAuthorCreate()
        {
            return Author.CreateAuthor("Test", "Testhest@haster.ko");

        }

        private static Post TestPostCreate(Author author)
        {

            Post p = new Post(author, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Test Title");
            return p;
        }
    }


    
}
