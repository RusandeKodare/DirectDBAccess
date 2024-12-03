namespace DirectDBAccess
{
    public class DatabaseService 
    {
        private readonly IDatabase _database;
        private readonly IIO _io;
        public DatabaseService(IIO io, IDatabase database)
        {
            _database = database;
            _io = io;
        }
        public void RunProgram()
        {
            
            _io.WriteLine("Hello user!");
            _io.WriteLine("What is you first name: ");
            string firstName = _io.ReadLine() ?? ""; ;
            _io.WriteLine("What is you last name: ");
            string lastName = _io.ReadLine() ?? "";
            _database.Insert(firstName, lastName);
        }
    }

}
