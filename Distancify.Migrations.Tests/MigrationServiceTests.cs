using NSubstitute;
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
            var sut = new MigrationService(new DefaultMigrationLocator(), new InMemoryMigrationLogFactory());

            sut.Apply<BAMigration>();

            Assert.Equal(3, SystemState.SomeValue);
        }

        [Fact]
        public void Apply_SkipAlreadyCommittedMigrations()
        {
            var log = new InMemoryMigrationLog();
            var logFactory = Substitute.For<IMigrationLogFactory>();
            logFactory.Create().ReturnsForAnyArgs(log);

            log.Commit(new B1Migration());

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory);

            sut.Apply<BAMigration>();

            Assert.Equal(2, SystemState.SomeValue);
        }

        [Fact]
        public void Apply_CommitUncommitedMigrations()
        {
            var log = new InMemoryMigrationLog();
            var logFactory = Substitute.For<IMigrationLogFactory>();
            logFactory.Create().ReturnsForAnyArgs(log);

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory);

            sut.Apply<BAMigration>();

            Assert.True(log.IsCommited(typeof(BA1Migration)));
        }
    }
}
