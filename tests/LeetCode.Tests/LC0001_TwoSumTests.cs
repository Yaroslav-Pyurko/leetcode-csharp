namespace LeetCode.Tests;

public class LC0001_TwoSumTests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
    public void TwoSum_ReturnsExpectedResult(int[] nums, int target, int[] expected) {
        var solution = new LC0001_TwoSum();

        var actual = solution.TwoSum(nums, target);

        Assert.Equal(expected, actual);
    }
}
