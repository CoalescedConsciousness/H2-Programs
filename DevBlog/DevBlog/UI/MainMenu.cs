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
                Console.WriteLine("### MAIN MENU ###");
                Console.WriteLine();
                Console.WriteLine("[AV] View Authors");
                Console.WriteLine("[AC] Create Author");
                Console.WriteLine("[AE] Edit Author");
                Console.WriteLine("[AM] View Authors in volatile memory");
                Console.WriteLine("[AT] Toggle Author");
                Console.WriteLine("---------------");
                Console.WriteLine("[PV] View Posts");
                Console.WriteLine("[PW] Write Post");
                Console.WriteLine("[PE] Edit Post");
                Console.WriteLine("[PL] Add Link to Post");
                Console.WriteLine("[PD] Erase Post");
                Console.WriteLine("[PM] View Posts in volatile memory");
                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("[X] Exit");
                
                string input = Console.ReadLine();

                MenuHelper.Selection choice; // Delegate instantiated

                // Author
                MenuHelper.GetSelection(input, "av", choice = ViewAuthors);
                MenuHelper.GetSelection(input, "ac", choice = AuthorCRUD.CreateAuthor);
                MenuHelper.GetSelection(input, "ae", choice = AuthorCRUD.Update);
                MenuHelper.GetSelection(input, "am", choice = Author.ReadAll);
                MenuHelper.GetSelection(input, "at", choice = Author.ToggleAuthor);

                // Posts
                MenuHelper.GetSelection(input, "pv", choice = ViewAllPosts);
                MenuHelper.GetSelection(input, "pw", choice = PostCRUD.CreatePost);
                MenuHelper.GetSelection(input, "pe", choice = EditPost);
                MenuHelper.GetSelection(input, "pl", choice = AddLinkToPost);
                MenuHelper.GetSelection(input, "pd", choice = PostCRUD.ErasePost);
                MenuHelper.GetSelection(input, "pm", choice = Post.ReadAll);
                // If "x", exit while loop.
                runMenu = input.ToLower() == "x" ? false : true;
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
                    PostURL.OpenURL(result);
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
                string urlChoice = Console.ReadLine();
                Console.WriteLine();
                
                UpdateURL(result, urlChoice); // Split this function for management and clarity purposes.
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
                PostURL.AddURL(target, newUrl);
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

                PostURL.RemoveURL(target, uInput);
                

            }
        }
    }
}
