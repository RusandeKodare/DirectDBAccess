using DirectDBAccess;
using Moq;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        private readonly Mock<IDatabase> _mockDatabase = new Mock<IDatabase>();

        [TestMethod]
        public void Insert_ShouldInsertStrings()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Doe";
            _mockDatabase.Setup(db => db.Insert(firstName,lastName)).Verifiable();
            //Act
            _mockDatabase.Object.Insert(firstName, lastName);
            //Assert
            _mockDatabase.Verify(db => db.Insert(firstName, lastName), Times.Once, "Insert method was not called correctly.");

        }
    }
}
