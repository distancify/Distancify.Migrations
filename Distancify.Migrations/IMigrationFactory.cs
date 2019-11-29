using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    public interface IMigrationFactory
    {
        IEnumerable<Migration> Create(IEnumerable<Type> migrations);
    }
}
