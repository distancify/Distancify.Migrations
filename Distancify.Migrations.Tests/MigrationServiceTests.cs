using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Distancify.Migrations.Tests
{
    public class MigrationServiceTests
    {
        public MigrationServiceTests()
        {
            SystemState.SomeValue = 0;
        }

        [Fact]
        public void Apply_ExecutesAllChanges()
        {
            var sut = new MigrationService(new DefaultMigrationLocator(), new InMemoryMigrationLog());

            sut.Apply<BAMigration>();

            Assert.Equal(3, SystemState.SomeValue);
        }

        [Fact]
        public void Apply_SkipAlreadyCommittedMigrations()
        {
            var log = new InMemoryMigrationLog();
            log.Commit(new B1Migration());

            var sut = new MigrationService(new DefaultMigrationLocator(), log);

            sut.Apply<BAMigration>();

            Assert.Equal(2, SystemState.SomeValue);
        }
    }
}
