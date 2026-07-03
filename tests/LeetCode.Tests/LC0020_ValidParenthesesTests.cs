namespace LeetCode.Tests
{
    public class LC0020_ValidParenthesesTests
    {
        [Theory]
        [InlineData("()", true)]
        [InlineData("()[]{}", true)]
        [InlineData("(]", false)]
        [InlineData("([])", true)]
        [InlineData("([)]", false)]
        public void IsValid_ReturnsExpectedResult(string input, bool expected)
        {
            var solution = new LC0020_ValidParentheses();

            var actual = solution.IsValid(input);

            Assert.Equal(expected, actual);
        }
    }
}