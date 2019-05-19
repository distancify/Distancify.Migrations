using System;
using System.Collections.Generic;

namespace Distancify.Migrations
{
    public interface IMigrationLocator
    {
        IEnumerable<Type> LocateAll<TBase>();
    }
}