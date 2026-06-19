namespace LeetCode.Tests
{
    public class LC0009_PalindromeNumberTests
    {
        [Theory]
        [InlineData(121, true)]
        [InlineData(-121, false)]
        [InlineData(10, false)]
        [InlineData(7, true)]
        public void IsPalindrome_ReturnsExpectedResult(int input, bool expected)
        {
            var solution = new LC0009_PalindromeNumber();
            var actual = solution.IsPalindrome(input);
            Assert.Equal(expected, actual);
        }
    }
}