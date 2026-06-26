namespace LeetCode.Tests
{
    public class LC0014_LongestCommonPrefixTests
    {
        [Theory]
        [InlineData(new string[] { "flower", "flow", "flight" }, "fl")]
        [InlineData(new string[] { "dog", "racecar", "car" }, "")]
        public void LongestCommonPrefix_ReturnsExpectedResult(string[] input, string expected)
        {
            var solution = new LC0014_LongestCommonPrefix();
            var actual = solution.LongestCommonPrefix(input);
            Assert.Equal(expected, actual);
        }
    }
}