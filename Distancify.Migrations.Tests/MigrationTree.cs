using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distancify.Migrations.Tests
{
    public static class SystemState
    {
        public static int SomeValue { get; set; }
    }

    public class TestMigration : Migration
    {
        public override void Apply()
        {

        }
    }

    public abstract class AMigration : Migration
    {
    }

    public class A1Migration : AMigration
    {
        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }

    public class A2Migration : AMigration
    {
        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BMigration : Migration
    {
    }

    public class B1Migration : BMigration
    {
        public override void Apply()
        {
            SystemState.SomeValue += 1;
        }
    }

    public abstract class BAMigration : BMigration
    {

    }

    public class BA1Migration : BAMigration
    {
        public override void Apply()
        {
            SystemState.SomeValue += 2;
        }
    }

    public abstract class CMigration : Migration
    {

    }

    [MigrationOrder("c")]
    public class C1Migration : CMigration
    {
        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }

    [MigrationOrder("a")]
    public class C2Migration : CMigration
    {
        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }

    [MigrationOrder("b")]
    public class C3Migration : CMigration
    {
        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }

    [DoNotCommit]
    public class DoNotCommitMigration : Migration
    {
        public override void Apply()
        {
            
        }
    }

    public abstract class FMigration : Migration
    {
    }

    [ForceMigration]
    public class F1Migration : FMigration
    {
        private int calls = 0;
        public override void Apply()
        {
            calls += 1;
        }

        public int Calls { get { return calls; } }
    }
}
