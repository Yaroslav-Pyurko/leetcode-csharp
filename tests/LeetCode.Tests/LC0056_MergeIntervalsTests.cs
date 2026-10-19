using System.Text.Json;

namespace LeetCode.Tests;

public class LC0056_MergeIntervalsTests
{
    [Theory]
    [InlineData(
        "[[1,3],[2,6],[8,10],[15,18]]",
        "[[1,6],      [8,10],[15,18]]")]
    [InlineData(
        "[[1,4],[4,5]]",
        "[[1,5]]")]
    [InlineData(
        "[[4,7],[1,4]]",
        "[[1,7]]")]
    [InlineData(
        "[[5,8]]",
        "[[5,8]]")]
    [InlineData(
        "[[1,10],[2,3],[4,5]]",
        "[[1,10]]")]
    [InlineData(
        "[[1,3],[1,5],[1,2]]",
        "[[1,5]]")]
    [InlineData(
        "[[1,1],[2,2]]",
        "[[1,1],[2,2]]")]
    [InlineData(
        "[[0,0],[0,0]]",
        "[[0,0]]")]
    [InlineData(
        "[[0,10000],[10000,10000]]",
        "[[0,10000]]")]
    public void Merge_ReturnsExpectedResult(string intervalsJson, string expectedJson)
    {
        var intervals = JsonSerializer.Deserialize<int[][]>(intervalsJson)!;
        var expected = JsonSerializer.Deserialize<int[][]>(expectedJson)!;
        var solution = new LC0056_MergeIntervals();

        var actual = solution.Merge(intervals);

        Assert.Equal(expected, actual);
    }
}
