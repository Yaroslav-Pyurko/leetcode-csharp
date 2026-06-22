namespace LeetCode.Tests
{
    public class LC0026_RemoveDuplicatesFromSortedArrayTests
    {
        [Theory]
        [InlineData(new int[] { 1, 1, 2 }, 2)]
        [InlineData(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5)]
        public void RemoveDuplicates_ReturnsExpectedResult(int[] input, int expected)
        {
            var solution = new LC0026_RemoveDuplicatesFromSortedArray();
            var actual = solution.RemoveDuplicates(input);
            Assert.Equal(expected, actual);
        }
    }
}