using LeetCode.Arrays;

namespace LeetCode.Arrays.Tests
{
    public class LC0014_LongestCommonPrefixTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0014_LongestCommonPrefix();
            var actual = solution.LongestCommonPrefix(["flower", "flow", "flight"]);
            var expected = "fl";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case2()
        {
            var solution = new LC0014_LongestCommonPrefix();
            var actual = solution.LongestCommonPrefix(["dog", "racecar", "car"]);
            var expected = "";
            Assert.Equal(expected, actual);
        }
    }
}