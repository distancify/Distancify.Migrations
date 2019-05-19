using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distancify.Migrations
{
    public interface IMigrationLog
    {
        IEnumerable<Type> GetCommitted();

        bool IsApplied(Type migration);

        void Commit(Migration migration);
    }
}
