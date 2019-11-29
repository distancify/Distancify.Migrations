using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    /// <summary>
    /// By applying this attribute to a migration, it will always be applied regardless of the MigrationLog.
    /// </summary>
    public class ForceMigrationAttribute : Attribute
    {
    }
}
