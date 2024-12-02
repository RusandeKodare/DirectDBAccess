using Microsoft.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string queryString = "INSERT INTO Person (firstName, lastName) VALUES (@firstName, @lastName)";
            using (_connection)
            {
                SqlCommand command = new SqlCommand(queryString, _connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
        }
    }
}
