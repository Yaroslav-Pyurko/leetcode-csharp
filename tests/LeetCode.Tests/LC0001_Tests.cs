using LeetCode.Arrays;

namespace LeetCode.Arrays.Tests
{
    public class LC0001_TwoSum_Tests
    {
        [Fact]
        public void Case1()
        {
            var s = new LC0001_TwoSum();

            var res = s.TwoSum([2, 7, 11, 15], 9);

            Assert.Equal(new[] { 0, 1 }, res);
        }
    }
}