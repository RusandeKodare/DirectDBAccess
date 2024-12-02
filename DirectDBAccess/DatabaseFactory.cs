﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDBAccess
{
    public class DatabaseFactory
    {
        private readonly string _connectionString;

        public DatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDatabase ChooseDb(int dbChoice)
        {
            return dbChoice switch
            {
                1 => new SqlServer(_connectionString),
                2 => new MongoDb(_connectionString),
                _ => throw new ArgumentException("Invalid choice. Please select a valid database type.")
            };
        }
    }

    public class DatabaseService
    {
        private readonly IDatabase _database;
        public DatabaseService(IDatabase database)
        {
            _database = database;
        }
        public void Insert(string firstName, string lastName)
        {
            _database.Insert(firstName, lastName);
        }
    }
}
