using DirectDBAccess;
using UnitTest.Mocks;

namespace UnitTest
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
    [TestClass]
    public class DatabaseServiceTest
    {
        [TestMethod]
        public void RunProgramTest()
        {
            //Arrange
            var dbMock = new DatabaseMock();
            var io = new MockIO(1);
            var dbService = new DatabaseService(io ,dbMock);
            //Act
            dbService.RunProgram();
            //Assert
            Assert.IsTrue(dbMock._databaseMock.Any(p=>p.FirstName == "John" && p.LastName == "Doe"));       
        }
    }
    [TestClass]
    public class SqlServerTest
    {
        [TestMethod]
        public void InsertTest()
        {
            //Arrange
            //Act
            //Assert
        }
        [TestMethod]
        public void ReadTest()
        {
            //Arrange
            //Act
            //Assert
          
        }
    }
}