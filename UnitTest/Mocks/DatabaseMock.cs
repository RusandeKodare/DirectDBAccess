using DirectDBAccess;
namespace UnitTest.Mocks
{
    internal class DatabaseMock : IDatabase
    {
        private readonly User? _user;
        public List<User> _databaseMock = new List<User>();

        public void Delete(string firstName, string lastName)
        {
        }
        public void Insert(string firstName, string lastName)
        {
            var user = new User (firstName, lastName);
            _databaseMock.Add(user);
        }
        public List<User> Read(string firstName, string lastName)
        {
            _databaseMock.Find(p => p.FirstName == firstName && p.LastName == lastName);
            return _databaseMock;
        }
        public void Update(string firstName, string lastName)
        {
        }
    }
}
