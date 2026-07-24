namespace LeetCode.Tests
{
    public class LC0001_TwoSumTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0001_TwoSum();

            var actual = solution.TwoSum([2, 7, 11, 15], 9);

            Assert.Equal(new[] { 0, 1 }, actual);
        }
    }
}