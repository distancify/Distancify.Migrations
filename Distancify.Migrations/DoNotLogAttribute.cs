using System;
using System.Collections.Generic;
using System.Text;

namespace Distancify.Migrations
{
    /// <summary>
    /// By applying this attribute to a migration, it will not be written to the migration log.
    /// This can be useful during development as you're iterating and want to apply the same migration
    /// over and over again until it's ready.
    /// </summary>
    public class DoNotCommitAttribute : Attribute
    {
    }
}
