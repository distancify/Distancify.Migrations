using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MigrationOrder : Attribute
    {
        public string Order { get; }

        public MigrationOrder(string order)
        {
            Order = order;
        }
    }
}
