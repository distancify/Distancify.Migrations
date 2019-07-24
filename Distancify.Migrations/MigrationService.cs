using System;
using System.Linq;

namespace Distancify.Migrations
{
    public class MigrationService
    {
        private readonly IMigrationLocator locator;
        private readonly IMigrationLogFactory logFactory;

        private readonly object _lock = new object();

        public MigrationService(IMigrationLocator locator, IMigrationLogFactory logFactory)
        {
            this.locator = locator;
            this.logFactory = logFactory;
        }

        public void Apply<T>()
        {
            lock (_lock)
            {
                using (var log = logFactory.Create())
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
    }
}
