using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Distancify.Migrations.Tests
{
    public class MigrationFactoryTests
    {
        [Fact]
        public void InstantiateMigration()
        {
            var sut = new MigrationFactory();

            var result = sut.Create(new List<Type> { typeof(B1Migration) })
                .ToList();

            Assert.IsType<B1Migration>(result.Single());
        }

        [Fact]
        public void InstantiateNonMigrationClass_Skip()
        {
            var sut = new MigrationFactory();

            var result = sut.Create(new List<Type> { typeof(string) })
                .ToList();

            Assert.Empty(result);
        }
    }
}
