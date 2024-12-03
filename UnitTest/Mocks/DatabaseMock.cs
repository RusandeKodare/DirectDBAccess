using DirectDBAccess;
namespace UnitTest.Mocks
{
    public class User
    {
        public List<User> Users { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
    internal class DatabaseMock : IDatabase
    {
        private readonly User _user;
        public List<User> _databaseMock = new List<User>();

        public void Delete(string firstName, string lastName)
        {

        }

        public void Insert(string firstName, string lastName)
        {
            var user = new User (firstName, lastName);
            _databaseMock.Add(user);
        }

        public void Read(string firstName, string lastName)
        {
            _databaseMock.Find(p => p.FirstName == firstName && p.LastName == lastName);

        }

        public void Update(string firstName, string lastName)
        {
            //Read();
        }
    }
}
