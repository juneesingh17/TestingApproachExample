using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System.Collections.Generic;

namespace TestingApproachUnitTest
{
   
    public class UnitTestWithFixtures : IDisposable
    {
        Stack<int> stack;

        public UnitTestWithFixtures()
        {
            stack = new Stack<int>();
        }

        public void Dispose()
        {
            stack.Clear();
        }

        [Fact]
        public void WithNoItems_CountShouldReturnZero()
        {
            var count = stack.Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public void AfterPushingItem_CountShouldReturnOne()
        {
            stack.Push(42);

            var count = stack.Count;

            Assert.Equal(1, count);
        }
    }
}
