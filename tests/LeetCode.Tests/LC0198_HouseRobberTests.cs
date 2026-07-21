namespace LeetCode.Tests
{
    public class LC0198_HouseRobberTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 }, 4)]
        [InlineData(new[] { 2, 7, 9, 3, 1 }, 12)]
        [InlineData(new[] { 5 }, 5)]
        [InlineData(new[] { 5, 1 }, 5)]
        [InlineData(new[] { 1, 5 }, 5)]
        [InlineData(new[] { 4, 4, 4, 4 }, 8)]
        [InlineData(new[] { 2, 1, 1, 2 }, 4)]
        [InlineData(new[] { 100, 1, 1, 100 }, 200)]
        [InlineData(new[] { 0, 0, 0 }, 0)]
        public void Rob_GivenHousesArray_ReturnsMaxNonAdjacentSum(int[] nums, int expected)
        {
            var solution = new LC0198_HouseRobber();

            var result = solution.Rob(nums);

            Assert.Equal(expected, result);
        }
    }
}