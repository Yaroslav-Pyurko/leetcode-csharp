namespace LeetCode.Tests
{
    public class LC0242_ValidAnagramTests
    {
        [Theory]
        [InlineData("anagram", "nagaram", true)] 
        [InlineData("rat",     "car",     false)]
        [InlineData("foobar", "foo", false)]
        [InlineData("longtextreallylongtext", "longtextreallylongtetx", true)]
        public void IsAnagram_ReturnsExpectedResult(string inputS, string inputT, bool expected)
        {
            var solution = new LC0242_ValidAnagram();

            var actual = solution.IsAnagram(inputS, inputT);

            Assert.Equal(expected, actual);
        }
    }
}