using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations.SqlMigration
{
    internal class MigrationLogContext : DbContext
    {
        public const string MigrationLogTableName = "__DistancifyMigrationLog";
        private readonly string _connectionString;

        public MigrationLogContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MigrationLog>(entity =>
            {
                entity.ToTable(MigrationLogTableName);
                entity.HasKey(p => p.Type);
                entity.Property(p => p.RunnedAt).IsRequired();
            });
        }

        public virtual DbSet<MigrationLog> MigrationLogs { get; set; }
    }
}
