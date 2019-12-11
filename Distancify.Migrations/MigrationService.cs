using System;
using System.Linq;

namespace Distancify.Migrations
{
    public class MigrationService
    {
        private readonly IMigrationLocator locator;
        private readonly IMigrationLogFactory logFactory;
        private readonly IMigrationFactory migrationFactory;
        private readonly object _lock = new object();

        public MigrationService(IMigrationLocator locator, IMigrationLogFactory logFactory, IMigrationFactory migrationFactory)
        {
            this.locator = locator;
            this.logFactory = logFactory;
            this.migrationFactory = migrationFactory;
        }

        public void Apply<T>()
        {
            lock (_lock)
            {
                using (var log = logFactory.Create())
                {
                    var migrations = migrationFactory.Create(
                        locator.LocateAll<T>().Where(r => HasForceAttribute(r) || !log.IsCommited(r)));

                    foreach (var m in migrations)
                    {
                        var commitToLog = ShouldCommitToLog(log, m);

                        Serilog.Log
                            .ForContext("CommitToLog", commitToLog)
                            .Information("Migrations: Applying {MigrationName}", m.GetType().Name);
                        m.Apply();

                        if (commitToLog)
                        {
                            log.Commit(m);
                        }
                    }
                }
            }

            bool ShouldCommitToLog(IMigrationLog log, Migration m)
            {
                var type = m.GetType();
                if (HasDoNotCommitAttribute(type))
                    return false;

                if (log.IsCommited(type))
                    return false;

                return true;
            }

            bool HasDoNotCommitAttribute(Type migration)
            {
                return migration.CustomAttributes.FirstOrDefault(r => r.AttributeType == typeof(DoNotCommitAttribute)) != null;
            }

            bool HasForceAttribute(Type migration)
            {
                return migration.CustomAttributes.FirstOrDefault(r => r.AttributeType == typeof(ForceMigrationAttribute)) != null;
            }
        }
    }
}
