namespace LeetCode.Tests
{
    public class LC0859_BuddyStringsTests
    {
        [Theory]
        [InlineData("ab", "ba", true)]
        [InlineData("ab", "ab", false)]
        [InlineData("aa", "aa", true)]
        [InlineData("longtextreallylongtext", "longtextreallylongtetx", true)]
        [InlineData("longtextreallylongtext", "longtextreallylongtxte", false)]
        public void BuddyStrings_ReturnsExpectedResult(string inputS, string inputT, bool expected)
        {
            var solution = new LC0859_BuddyStrings();

            var actual = solution.BuddyStrings(inputS, inputT);

            Assert.Equal(expected, actual);
        }
    }
}