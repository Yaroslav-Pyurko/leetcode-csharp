namespace LeetCode.Tests
{
    public class LC0026_RemoveDuplicatesFromSortedArrayTests
    {
        [Theory]
        [InlineData(new int[] { 1, 1, 2 }, 2, new int[] { 1, 2 })]
        [InlineData(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5, new int[] { 0, 1, 2, 3, 4 })]
        public void RemoveDuplicates_ReturnsExpectedResult(int[] input, int expected, int[] expectedArray)
        {
            var solution = new LC0026_RemoveDuplicatesFromSortedArray();
            var actual = solution.RemoveDuplicates(input);

            // Check arrays
            int minLength = Math.Min(input.Length, expectedArray.Length);
            var areEqual = input.Take(minLength).SequenceEqual(expectedArray.Take(minLength));
            Assert.True(areEqual);

            // Check count
            Assert.Equal(expected, actual);
        }
    }
}