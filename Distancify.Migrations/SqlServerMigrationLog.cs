using Distancify.Migrations.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Distancify.Migrations
{
    public class SqlServerMigrationLog : IMigrationLog
    {
        private readonly MigrationLogContext _migrationLogContext;

        public SqlServerMigrationLog(string connectionString)
        {
            _migrationLogContext = new MigrationLogContext(connectionString);
            _migrationLogContext.Database.Migrate();
        }

        public void Commit(Migration migration)
        {
            _migrationLogContext.MigrationLogs.Add(new MigrationLog
            {
                Type = migration.GetType().Name,
                RunnedAt = DateTime.UtcNow
            });

            _migrationLogContext.SaveChanges();
        }

        public void Dispose()
        {
            _migrationLogContext.Dispose();
        }

        public IEnumerable<Type> GetCommitted()
        {
            return _migrationLogContext.MigrationLogs.Select(m => Type.GetType(m.Type));
        }

        public bool IsCommited(Type migration)
        {
            return _migrationLogContext.MigrationLogs.Any(m => m.Type.Equals(migration.Name));
        }
    }
}
