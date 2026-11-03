namespace LeetCode.Tests;

public class LC0042_TrappingRainWaterTests
{
    private const int MaxLength = 20_000;
    private const int MaxHeight = 100_000;

    [Theory]
    [InlineData(
        new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 },
        6)]
    [InlineData(
        new int[] { 4, 2, 0, 3, 2, 5 },
        9)]
    [InlineData(
        new int[] { 5 },
        0)]
    [InlineData(
        new int[] { 3, 0 },
        0)]
    [InlineData(
        new int[] { 1, 2, 3, 4 },
        0)]
    [InlineData(
        new int[] { 4, 3, 2, 1 },
        0)]
    [InlineData(
        new int[] { 3, 3, 3 },
        0)]
    [InlineData(
        new int[] { 2, 0, 0, 2 },
        4)]
    [InlineData(
        new int[] { 5, 0, 3 },
        3)]
    [InlineData(
        new int[] { 3, 0, 2, 0, 4 },
        7)]
    public void Trap_ReturnsExpectedResult(int[] height, int expected)
    {
        var solution = new LC0042_TrappingRainWater();

        var actual = solution.Trap(height);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Trap_TallestWallsAroundLongestValley_FitsInInt()
    {
        var height = new int[MaxLength];
        height[0] = MaxHeight;
        height[^1] = MaxHeight;
        var solution = new LC0042_TrappingRainWater();

        var actual = solution.Trap(height);

        Assert.Equal((MaxLength - 2) * MaxHeight, actual);
    }
}
