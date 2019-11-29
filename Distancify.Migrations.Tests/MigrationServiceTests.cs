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

        private IMigrationFactory GetMigrationFactorySubstitute()
        {
            var r = Substitute.For<IMigrationFactory>();
            r.Create(Arg.Any<IEnumerable<Type>>())
                .Returns((ci) => ci.Arg<IEnumerable<Type>>().Select(m => (Migration)Activator.CreateInstance(m)));
            return r;
        }

        [Fact]
        public void Apply_ExecutesAllChanges()
        {
            var sut = new MigrationService(new DefaultMigrationLocator(), new InMemoryMigrationLogFactory(), GetMigrationFactorySubstitute());

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

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory, GetMigrationFactorySubstitute());

            sut.Apply<BAMigration>();

            Assert.Equal(2, SystemState.SomeValue);
        }

        [Fact]
        public void Apply_CommitUncommitedMigrations()
        {
            var log = new InMemoryMigrationLog();
            var logFactory = Substitute.For<IMigrationLogFactory>();
            logFactory.Create().ReturnsForAnyArgs(log);

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory, GetMigrationFactorySubstitute());

            sut.Apply<BAMigration>();

            Assert.True(log.IsCommited(typeof(BA1Migration)));
        }

        [Fact]
        public void DoNotCommitAttribute()
        {
            var log = new InMemoryMigrationLog();
            var logFactory = Substitute.For<IMigrationLogFactory>();
            logFactory.Create().ReturnsForAnyArgs(log);

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory, GetMigrationFactorySubstitute());

            sut.Apply<DoNotCommitMigration>();

            Assert.False(log.IsCommited(typeof(DoNotCommitMigration)));
        }

        [Fact]
        public void CallsMigrationFactory()
        {
            var log = new InMemoryMigrationLog();
            var logFactory = Substitute.For<IMigrationLogFactory>();
            logFactory.Create().ReturnsForAnyArgs(log);
            var migrationFactory = GetMigrationFactorySubstitute();

            var sut = new MigrationService(new DefaultMigrationLocator(), logFactory, migrationFactory);

            sut.Apply<B1Migration>();

            migrationFactory.Received().Create(Arg.Any<IEnumerable<Type>>());
        }
    }
}
