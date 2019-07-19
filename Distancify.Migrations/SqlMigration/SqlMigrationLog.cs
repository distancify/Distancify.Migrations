using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Distancify.Migrations.SqlMigration
{
    public class SqlMigrationLog : IMigrationLog
    {
        private readonly MigrationLogContext _migrationLogContext;

        public SqlMigrationLog(string connectionString)
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
