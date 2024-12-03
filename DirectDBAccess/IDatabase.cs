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
        public List<User> Read(string firstName, string lastName);
    }
    public class SqlServer : IDatabase
    {
        private readonly SqlConnection _connection;
        private List<User>? _users;
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
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            Debug.WriteLine("Success");
        }
        public List<User> Read(string firstName, string lastName)
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    _connection.Open();
                }
                string queryString = $"SELECT FirstName, LastName FROM Person WHERE FirstName = @firstName AND LastName = @lastName";
                using (SqlCommand command = new SqlCommand(queryString, _connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        bool found = false;
                        int index = 1;
                        while (reader.Read())
                        {
                            string retrievedFirstName = reader["FirstName"].ToString() ?? "";
                            string retrievedLastName = reader["LastName"].ToString()??"";
                            var user = new User(retrievedFirstName, retrievedLastName);
                            users.Add(user);
                            found = true;
                        }
                        Console.WriteLine($"Found following users with first name {firstName} and last name {lastName}\n");

                        foreach (var item in users)
                        {
                            Console.WriteLine($"User {index}: {item.FirstName} {item.LastName}");
                            index++;
                        }
                        if (!found)
                        {
                            Console.WriteLine("No matching record found.");
                        }
                    }
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
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            Debug.WriteLine("Success");
            return _users??new();
        }
        public void Update(string firstName, string lastName)
        {  
        }
    }
    public class MongoDb : IDatabase
    {
        private readonly IMongoDatabase? _database;

        private readonly IMongoCollection<BsonDocument>? _collection;
        public MongoDb(string connectionString)
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
                _collection?.InsertOne(document);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("Success");
        }
        public List<User> Read(string firstName, string lastName)
        {
            return new List<User>();
        }

        public void Update(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
