namespace LeetCode.Tests;

public class LC0042_TrappingRainWaterTests
{
    [Theory]
    [InlineData(
        new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 },
        6)]
    [InlineData(
        new int[] { 4, 2, 0, 3, 2, 5 },
        9)]
    public void Trap_ReturnsExpectedResult(int[] height, int expected)
    {
        var solution = new LC0042_TrappingRainWater();

        var actual = solution.Trap(height);

        Assert.Equal(expected, actual);
    }
}
