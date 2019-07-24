using System;

namespace Distancify.Migrations.EntityFramework
{
    internal class MigrationLog
    {
        public string Type { get; set; }
        public DateTime RunnedAt { get; set; }
    }
}
