using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Distancify.Migrations.Tests
{
    public class InMemoryMigrationLogTests
    {
        [Fact]
        public void GetCommitted_GetsAllPreviouslyCommittedMigrations()
        {
            var sut = new InMemoryMigrationLog();
            sut.Commit(new BA1Migration());
            sut.Commit(new B1Migration());
            var result = sut.GetCommitted();

            Assert.Equal(2, result.Count());
            Assert.Contains(typeof(BA1Migration), result);
            Assert.Contains(typeof(B1Migration), result);
        }


        [Fact]
        public void IsApplied_ReturnsTrueIfCommitted()
        {
            var sut = new InMemoryMigrationLog();
            sut.Commit(new BA1Migration());
            Assert.True(sut.IsApplied(typeof(BA1Migration)));
        }

        [Fact]
        public void IsApplied_ReturnsFalseIfNotCommitted()
        {
            var sut = new InMemoryMigrationLog();
            sut.Commit(new B1Migration());
            Assert.False(sut.IsApplied(typeof(BA1Migration)));
        }

        [Fact]
        public void Commit_SameTypeTwice_OnlyAddOnce()
        {
            var sut = new InMemoryMigrationLog();
            sut.Commit(new BA1Migration());
            sut.Commit(new BA1Migration());
            var result = sut.GetCommitted();

            Assert.Equal(1, result.Count());
            Assert.Contains(typeof(BA1Migration), result);
        }
    }
}
