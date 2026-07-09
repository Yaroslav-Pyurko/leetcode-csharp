using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Tests
{
    public class LC2235_AddTwoIntegersTests
    {
        [Theory]
        [InlineData(12, 5, 17)]
        [InlineData(-10, 4, -6)]
        public void Sum_ReturnsExpectedResult(int input1, int input2, int expected)
        {
            var solution = new LC2235_AddTwoIntegers();

            var actual = solution.Sum(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}