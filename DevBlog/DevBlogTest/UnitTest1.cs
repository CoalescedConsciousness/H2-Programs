using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevBlog;
using DevBlog.Repository;
using Common;

namespace DevBlogTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string name = "Test Author";
            string email = "test@email.com";

            string[] author = { name, email };
            
            // Act
            Author a = AuthorCRUD.CreateAuthor(author);
            string[] post = { a.ID.ToString(), "Test body", "Test Title" };
            System.Console.WriteLine(a.PostCount);
            PostCRUD.CreatePost(post);
            int i = DatabaseHelper.GetNextID("Author");

            // Assert
            Assert.AreEqual(1, a.PostCount);
            Assert.AreEqual(true, a.Active);
            Assert.AreEqual(i - 1, a.ID);
            Assert.AreEqual(i, a.ID + 1);
        }
    }
}