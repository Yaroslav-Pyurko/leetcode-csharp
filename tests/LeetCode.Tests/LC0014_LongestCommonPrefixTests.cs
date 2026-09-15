namespace LeetCode.Tests
{
    public class LC0014_LongestCommonPrefixTests
    {
        [Theory]
        [InlineData(new string[] { "flower", "flow", "flight" }, "fl")]
        [InlineData(new string[] { "dog", "racecar", "car" }, "")]
        [InlineData(new string[] { "interview", "inter", "interest" }, "inter")]
        [InlineData(new string[] { "a" }, "a")]
        [InlineData(new string[] { "abc", "abc" }, "abc")]
        [InlineData(new string[] { "abc", "ab" }, "ab")]
        [InlineData(new string[] { "ab", "abc" }, "ab")]
        [InlineData(new string[] { "", "b" }, "")]
        [InlineData(new string[] { "b", "" }, "")]
        public void LongestCommonPrefix_ReturnsExpectedResult(string[] input, string expected)
        {
            var solution = new LC0014_LongestCommonPrefix();

            var actual = solution.LongestCommonPrefix(input);

            Assert.Equal(expected, actual);
        }
    }
}
