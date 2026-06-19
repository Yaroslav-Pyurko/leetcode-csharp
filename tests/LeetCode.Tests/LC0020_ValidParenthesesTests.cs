namespace LeetCode.Tests
{
    public class LC0020_ValidParenthesesTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0020_ValidParentheses();
            var actual = solution.IsValid("()");
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case2()
        {
            var solution = new LC0020_ValidParentheses();
            var actual = solution.IsValid("()[]{}");
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case3()
        {
            var solution = new LC0020_ValidParentheses();
            var actual = solution.IsValid("(]");
            var expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case4()
        {
            var solution = new LC0020_ValidParentheses();
            var actual = solution.IsValid("([])");
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case5()
        {
            var solution = new LC0020_ValidParentheses();
            var actual = solution.IsValid("([)]");
            var expected = false;
            Assert.Equal(expected, actual);
        }
    }
}