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

            var migrations = locator.LocateAll<T>()
                .Where(r => !log.IsCommited(r))
                .Select(r => Activator.CreateInstance(r))
                .OfType<Migration>();

            foreach (var m in migrations)
            {
                Serilog.Log.Information("Migrations: Applying {MigrationName}", m.GetType().Name);
                m.Apply();
                log.Commit(m);
            }
        }
    }
}
