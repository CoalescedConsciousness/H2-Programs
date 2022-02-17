using Common;
using System.Diagnostics;
using DevBlog.Repository;

namespace DevBlog
{
    public record Program
    {
        static void Main(string[] args)
        {
            Init();

            Menu.Run();
            // Lonelyyyy, I'm so lonelyyyyy,
            // I've got nobodyyyyy,
            // To call my ooooOOOOOwn.

            Conclude();
        }

        private static async void Init()
        {
            //AuthorCRUD.LoadAsync();
            //PostCRUD.LoadAsync();
            await Task.Run(() => AuthorCRUD.LoadAsync());
            await Task.Run(() => PostCRUD.LoadAsync());
        }

        private static void Conclude()
        {
            AuthorCRUD.Save();
            PostCRUD.Save();
        }
    }
}