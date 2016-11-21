﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace TestingApproachUnitTest
{
    [Collection("Database collection")]
    public class DatabaseTestClass1
    {
        DatabaseFixture fixture;
        public DatabaseTestClass1(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestMethod1()
        {            
            Xunit.Assert.Equal(1, fixture.ReturnRows().Rows.Count);
        }
    }
}
