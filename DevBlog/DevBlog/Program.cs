using Common;
using System.Diagnostics;

namespace DevBlog
{
    public record Program
    {
        static void Main(string[] args)
        {
            Menu.CreateTestData();
            Storage.PostStorage.Load();
            Menu.Run();
            // Lonelyyyy, I'm so lonelyyyyy,
            // I've got nobodyyyyy,
            // To call my ooooOOOOOwn.
        }
    }
}