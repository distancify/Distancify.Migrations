using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    public class InMemoryMigrationLogFactory : IMigrationLogFactory
    {
        public IMigrationLog Create()
        {
            return new InMemoryMigrationLog();
        }
    }
}
