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

        [Fact]
        public void Case4()
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(2_147_483_647); // 2 ^ 32 - 1
            var expected = 0; // переполнение Int32 746_384_741_2;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case5()
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(111_222_333);
            var expected = 333_222_111;
            Assert.Equal(expected, actual);
        }
    }
}