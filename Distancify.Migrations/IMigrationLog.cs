using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distancify.Migrations
{
    public interface IMigrationLog : IDisposable
    {
        IEnumerable<Type> GetCommitted();

        bool IsCommited(Type migration);

        void Commit(Migration migration);
    }
}
