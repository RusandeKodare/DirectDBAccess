using DirectDBAccess;

namespace UnitTest.Mocks
{
    internal class DatabaseMock : IDatabase
    {
        private readonly List<string> _databaseMock = new List<string>();
        private bool _successful;

        public DatabaseMock(bool successful)
        {
            _successful = successful;
        }
        public void Insert(string firstName, string lastName)
        {
            _databaseMock.Add(firstName);
            _databaseMock.Add(lastName);

            if (_databaseMock.Contains(firstName)&& _databaseMock.Contains(lastName))
            {
                _successful = true;
            }
            else
            {
                _successful = true;
            }
        }
    }
}
