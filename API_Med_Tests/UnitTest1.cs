using API_Med.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API_Med_Tests
{
    [TestClass]
    public class API_Med_Repository_Test
    {
        [TestMethod]
        public void GetEventById_GettingNonExistentId_ReturnsNull()
        {
            //Arrange
            var repository = new SQLAPIRepo();
            //Act

            //Assert

        }
    }
}
