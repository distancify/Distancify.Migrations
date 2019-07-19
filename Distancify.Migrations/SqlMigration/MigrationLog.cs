using System;

namespace Distancify.Migrations.SqlMigration
{
    public class MigrationLog
    {
        public string Type { get; set; }
        public DateTime RunnedAt { get; set; }
    }
}
