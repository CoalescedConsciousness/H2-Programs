using Common;

namespace DevBlog
{
    public class Menu
    {
        /// <summary>
        /// Used to initiate Menus.
        /// </summary>
        public static async Task Run()
        {
            
            await TestMenu.RunAsync();
            MainMenu.Run();
        }
        
        
    }


    
}
