using Common;
using DevBlog.Repository;

namespace DevBlog
{
    public class MainMenu
    {

        /// <summary>
        /// Initiates the main Menu UI
        /// </summary>
        internal static void Run()
        {
            
            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Choose test option");
                Console.WriteLine("[A] View Authors");
                Console.WriteLine("[P] View Posts");
                Console.WriteLine("[W] Write Post");
                Console.WriteLine("[E] Edit Post");
                Console.WriteLine("[L] Add Link to Post");
                Console.WriteLine("[X] Exit");

                string input = Console.ReadLine();

                MenuHelper.Selection choice; // Delegate instantiated

                MenuHelper.GetSelection(input, "a", choice = ViewAuthors);
                MenuHelper.GetSelection(input, "p", choice = ViewAllPosts);
                MenuHelper.GetSelection(input, "w", choice = PostCRUD.PostMessage);
                MenuHelper.GetSelection(input, "e", choice = EditPost);
                MenuHelper.GetSelection(input, "l", choice = AddLinkToPost);
                // If "s", exit while loop.
                runMenu = input.ToLower() == "x" ? false : true;
            }

            runMenu = true;
            while (runMenu)
            {
                Console.Clear();
            }
        }

        // Various selection methods
        internal static void ViewAuthors()
        {
            Console.Clear();
            Database.GetAllFromDatabase("Author");
            Console.ReadLine();
        }

        internal static void ViewAllPosts()
        {
            Console.Clear();
            Database.GetAllFromDatabase("Post");
            Console.WriteLine("\nIf you wish to view a specific post, please input the ID, otherwise, press any key.\n");
            ViewSinglePost(Console.ReadLine());
        }

        internal static bool ViewSinglePost(string input)
        {

            if (int.TryParse(input, out int result))
            {
                DatabaseHelper.GetRecord("Post", result);

                Console.WriteLine("Would you like to view a link? [y/n]");

                if (Console.ReadLine().ToLower() == "y")
                {
                    PostCRUD.OpenURL(result);
                }
                return true;
            }
            return false;
        }

        internal static void EditPost()
        {
            Console.Clear();
            Console.WriteLine("Please select the post you wish to edit:\n\n");
            Database.GetAllFromDatabase("Post");
            string input = Console.ReadLine();
            if (ViewSinglePost(input))
            {
                PostCRUD.Update(int.Parse(input));
            }
        }

        internal static void AddLinkToPost()
        {
            Console.Clear();
            Console.WriteLine("Please select the post you wish to add a link to:\n\n");
            Database.GetAllFromDatabase("Post");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                Console.Clear();
                Console.WriteLine("Please select one:");
                Console.WriteLine("[A] Add new URL");
                Console.WriteLine("[R] Remove existing URL");
                Console.WriteLine("Please enter a valid URL:");
                string urlChoice = Console.ReadLine();
                
                UpdateURL(result, urlChoice); // Split this functino for management and clarity purposes.
            }
        }
        public static void UpdateURL(int id, string input)
        {
            Post target = Post.GetPost(id);

            //// Add URL
            if (input.ToLower() == "a") 
            {
                Console.WriteLine("Please designate the URL you want to add:");
                string newUrl = Console.ReadLine();
                PostCRUD.AddURL(target, newUrl);
            }
            
            //// Remove URL
            if (input.ToLower() == "r") 
            {
                Console.WriteLine("Please designate which URL you want to remove:");
                
                if (!target.Links.Contains(";"))
                {
                    Console.WriteLine($"0- {target.Links}");
                    
                }
                else
                {
                    for (int i = 0; i < target.Links.Split(";").Length; i++)
                    {
                        Console.WriteLine($"{i}- {target.Links.Split(";")[i]}");
                    }
                }
                int uInput = Console.Read();

                PostCRUD.RemoveURL(target, uInput);
                

            }
        }
    }
}
