namespace LeetCode.Tests
{
    public class LC0217_ContainsDuplicateTests
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 1 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4 }, false)]
        [InlineData(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
        [InlineData(new int[] { 1 }, false)]
        [InlineData(new int[] { }, false)]
        public void ContainsDuplicate_ReturnsExpectedResult(int[] input, bool expected)
        {
            var solution = new LC0217_ContainsDuplicate();
            var actual = solution.ContainsDuplicate(input);
            Assert.Equal(expected, actual);
        }
    }
}