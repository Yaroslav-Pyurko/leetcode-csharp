namespace LeetCode.Tests
{
    public class LC0007_ReverseIntegerTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(123);
            var expected = 321;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case2()
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(-123);
            var expected = -321;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case3()
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(120);
            var expected = 21;
            Assert.Equal(expected, actual);
        }
    }
}