using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestConcert
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string fName = "First";
            string lName = "Last";
      
            // Act
            Concert.Attendees nAtt = new Concert.Attendees(fName, lName);
            Concert.Attendees secondAtt = new Concert.Attendees(fName, lName);

            //Assert
            Assert.AreEqual(nAtt.FullName, ($"{fName} {lName}"));
            Assert.AreEqual(nAtt.AttID, 0);
            Assert.AreEqual(secondAtt.AttID, 1);


        }
    }
}