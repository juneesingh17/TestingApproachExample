using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace TestingApproachUnitTest
{
    public class MyDatabaseTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;

        public MyDatabaseTests(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        //[Fact]
        //public void TestMethod1()
        //{
        //    Xunit.Assert.True(fixture.HasRows(fixture.DbConnection));            
        //}
    }
}
