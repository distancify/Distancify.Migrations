using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Distancify.Migrations
{
    public class MigrationFactory : IMigrationFactory
    {
        public IEnumerable<Migration> Create(IEnumerable<Type> migrations)
        {
            return migrations.Where(t => typeof(Migration).IsAssignableFrom(t))
                .Select(m => (Migration)Activator.CreateInstance(m));
        }
    }
}
