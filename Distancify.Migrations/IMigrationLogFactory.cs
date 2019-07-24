using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    public interface IMigrationLogFactory
    {
        IMigrationLog Create();
    }
}
