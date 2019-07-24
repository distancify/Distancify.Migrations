using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distancify.Migrations
{
    public class InMemoryMigrationLog : IMigrationLog
    {
        private readonly ISet<Type> _log = new HashSet<Type>(); 

        public void Commit(Migration migration)
        {
            _log.Add(migration.GetType());
        }

        public void Dispose()
        {
        }

        public IEnumerable<Type> GetCommitted()
        {
            return _log;
        }

        public bool IsCommited(Type migration)
        {
            return _log.Contains(migration);
        }
    }
}
