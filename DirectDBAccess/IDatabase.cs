using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;

namespace DirectDBAccess
{
    public interface IDatabase
    {
        public void Insert(string firstName, string lastName);
        public void Update(string firstName, string lastName);
        public void Delete(string firstName, string lastName);
        public void Read(string firstName, string lastName);
    }
    public class SqlServer : IDatabase
    {
        private readonly SqlConnection _connection;
        public SqlServer(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Delete(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public void Insert(string firstName, string lastName)
        {
            try
            {
                _connection.Open();
                string queryString = $"INSERT INTO Person (FirstName, LastName) VALUES (@firstName, @lastName)";
                using (_connection)
                {
                    SqlCommand command = new SqlCommand(queryString, _connection);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Exception occurred communicating with database {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Debug.WriteLine("Success");
        }

        public void Read(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public void Update(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
    public class CloudMongoDb : IDatabase
    {
        private readonly IMongoDatabase? _database;
        private readonly IMongoCollection<BsonDocument>? _collection;
        public CloudMongoDb(string connectionString)
        {
            try
            {
                var client = new MongoClient(connectionString);
                _database = client.GetDatabase("Person");
                _collection = _database.GetCollection<BsonDocument>($"Person");
            }
            catch (MongoException ex)
            {
                Console.WriteLine("Exception occurred communicating with database", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
            }
            Debug.WriteLine("Successful connection");
        }

        public void Delete(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public void Insert(string firstName, string lastName)
        {
            try
            {
                var document = new BsonDocument
                {
                    { "FirstName", firstName},
                    { "LastName", lastName}
                };
                _collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("Success");
        }

        public void Read(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public void Update(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }

}
