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
    }
}

/*

Input: s = "(]"
Output: false

Input: s = "([])"
Output: true

Input: s = "([)]"
Output: false
*/