using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Distancify.Migrations
{
    public class DefaultMigrationLocator : IMigrationLocator
    {
        private IList<string> _assemblyIgnoreList = new List<string>
        {
            "Microsoft.",
            "xunit.",
            "System",
            "mscorlib"
        };

        public IEnumerable<Type> LocateAll<TBase>()
        {
            var result = new List<Type>();

            Type c = typeof(TBase);
            do
            {
                result.AddRange(GetChildren(c));
            }
            while ((c = c.BaseType) != typeof(object));

            return result.OrderBy(r => {
                var orderAttribute = r.GetCustomAttributes(false).FirstOrDefault(a => a is MigrationOrder);
                return orderAttribute != null ? ((MigrationOrder)orderAttribute).Order : r.Name;
            });
        }

        private IEnumerable<Type> GetChildren(Type type)
        {
            var result = new List<Type>();

            foreach (var t in GetAssemblies())
            {
                result.AddRange(
                    t.GetTypes()
                    .Where(r => r.BaseType == type)
                    .Where(r => !r.IsAbstract));
            }

            return result;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(r => !_assemblyIgnoreList.Any(i => r.FullName.StartsWith(i)));
        }
    }
}
