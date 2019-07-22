using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Distancify.Migrations;

namespace Distancify.Migrations.Tests
{
    public class DefaultMigrationLocatorTests
    {

        [Fact]
        public void Locate_AllDirectlyInheritingMigrations()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<Migration>();

            Assert.Contains(typeof(TestMigration), result);
        }

        [Fact]
        public void Locate_OnlyReturnDirectInheritance()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<Migration>();

            Assert.DoesNotContain(typeof(A1Migration), result);
        }

        [Fact]
        public void Locate_IgnoreAbstractMigrations()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<Migration>();

            Assert.DoesNotContain(typeof(AMigration), result);
        }

        [Fact]
        public void Locate_GetTaggedMigrationsButNotOtherTags()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<AMigration>();

            Assert.Contains(typeof(A1Migration), result);
            Assert.DoesNotContain(typeof(B1Migration), result);
        }


        [Fact]
        public void Locate_GetIndirectlyTaggedMigrations()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<BAMigration>();

            Assert.Contains(typeof(B1Migration), result);
        }

        [Fact]
        public void Locate_OrderByAttribute()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<CMigration>().ToList();

            var expected = new List<Type>
            {
                typeof(C2Migration),
                typeof(C3Migration),
                typeof(C1Migration),
                typeof(TestMigration)
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Locate_OrderByClassNameIfNoAttribute()
        {
            var sut = new DefaultMigrationLocator();
            var result = sut.LocateAll<BAMigration>().ToList();

            var expected = new List<Type>
            {
                typeof(B1Migration),
                typeof(BA1Migration),
                typeof(TestMigration)
            };

            Assert.Equal(expected, result);
        }
    }
}
