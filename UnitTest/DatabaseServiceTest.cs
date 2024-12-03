using DirectDBAccess;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestClass]
    public class DatabaseServiceTest
    {
        [TestMethod]
        public void RunProgramTest()
        {
            //Arrange
            var dbMock = new DatabaseMock();
            var io = new MockIO();
            var dbService = new DatabaseService(io ,dbMock);
            //Act
            dbService.RunProgram();
            //Assert
            Assert.IsTrue(dbMock._databaseMock.Any(p=>p.FirstName == "John" && p.LastName == "John"));
            
        }
    }

    
}