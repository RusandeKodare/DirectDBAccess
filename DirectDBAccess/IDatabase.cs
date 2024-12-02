using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DirectDBAccess
{
    public interface IDatabase
    {
        public void Insert(string firstName, string lastName);
    }

    public class SqlServer : IDatabase
    {
        private readonly SqlConnection _connection;

        public SqlServer(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Insert(string firstName, string lastName)
        {
            _connection.Open();
            string queryString = "INSERT INTO Person (firstName, lastName) VALUES (@firstName, @lastName)";
            using (_connection)
            {
                SqlCommand command = new SqlCommand(queryString, _connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Success");
        }
    }

    public class MongoDb : IDatabase
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _collection;
        public MongoDb(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Person");
            _collection = _database.GetCollection<BsonDocument>("Jobs");
        }
        public void Insert(string firstName, string lastName)
        {
            var document = new BsonDocument
        {
            { "FirstName", firstName },
            { "LastName", lastName }
        };

            _collection.InsertOne(document);
            Console.WriteLine("Success");
        }
    }
}
