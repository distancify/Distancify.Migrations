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
}
