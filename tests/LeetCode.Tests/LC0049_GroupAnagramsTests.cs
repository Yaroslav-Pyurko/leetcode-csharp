namespace LeetCode.Tests
{
    public class LC0049_GroupAnagramsTests
    {
        [Theory]
        [InlineData(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" }, "bat", "nat,tan", "ate,eat,tea")]
        [InlineData(new string[] { "" }, "")]
        [InlineData(new string[] { "a" }, "a")]
        [InlineData(new string[] { "^:;!@&*" }, "^:;!@&*")]
        public void Test_WithFlattenedData(string[] inputArray, params string[] expectedGroups)
        {
            List<List<string>> expected = expectedGroups
                .Select(group => group
                    .Split(',')
                    .ToList())
                .ToList();
            var solution = new LC0049_GroupAnagrams();

            var actual = solution.GroupAnagrams(inputArray);

            var normalizedExpected = expected
                .Select(group => group.OrderBy(word => word).ToList())
                .OrderBy(group => group.FirstOrDefault() ?? string.Empty)
                .ToList();
            var normalizedActual = actual
                .Select(group => group.OrderBy(word => word).ToList())
                .OrderBy(group => group.FirstOrDefault() ?? string.Empty)
                .ToList();
            Assert.Equal(normalizedExpected, normalizedActual);
        }
    }
}