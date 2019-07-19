using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations.SqlMigration
{
    public class MigrationLogContextFactory : IDesignTimeDbContextFactory<MigrationLogContext>
    {
        public MigrationLogContext CreateDbContext(string[] args)
        {
            return new MigrationLogContext("Server=localhost;Database=LitiumDB_KidsConcept.Web;Trusted_Connection=True;");
        }
    }
}
