using Distancify.Migrations.SqlMigration;
using System;
using System.Linq;

namespace Distancify.Migrations
{
    public class MigrationService
    {
        private readonly IMigrationLocator locator;
        private readonly IMigrationLog log;

        public MigrationService(IMigrationLocator locator, IMigrationLog log)
        {
            this.locator = locator;
            this.log = log;
        }

        public void Apply<T>()
        {
            var logg = new SqlMigrationLog("Server=localhost;Database=LitiumDB_KidsConcept.Web;Trusted_Connection=True;");

            var migrations = locator.LocateAll<T>()
                .Where(r => !logg.IsCommited(r))
                .Select(r => Activator.CreateInstance(r))
                .OfType<Migration>();

            foreach (var m in migrations)
            {
                m.Apply();
                logg.Commit(m);
            }
        }
    }
}
