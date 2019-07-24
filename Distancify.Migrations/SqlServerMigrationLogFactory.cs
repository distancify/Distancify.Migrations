using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    public class SqlServerMigrationLogFactory : IMigrationLogFactory
    {
        private readonly string _connectionString;

        public SqlServerMigrationLogFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IMigrationLog Create()
        {
            return new SqlServerMigrationLog(_connectionString);
        }
    }
}
