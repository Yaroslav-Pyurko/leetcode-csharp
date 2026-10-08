namespace LeetCode.Tests;

public class LC0056_MergeIntervalsTests
{
    public static IEnumerable<object[]> TestCases()
    {
        yield return new object[]
        {
            new int[][] { new int[] { 1, 3 }, new int[] { 2, 6 }, new int[] { 8, 10 }, new int[] { 15, 18 } },
            new int[][] { new int[] { 1, 6 },                     new int[] { 8, 10 }, new int[] { 15, 18 } }
        };

        yield return new object[]
        {
            new int[][] { new int[] { 1, 4 }, new int[] { 4, 5 } },
            new int[][] { new int[] { 1, 5 } }
        };

        yield return new object[]
        {
            new int[][] { new int[] { 4, 7 }, new int[] { 1, 4 } },
            new int[][] { new int[] { 1, 7 } }
        };
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Merge_ReturnsExpectedResult(int[][] intervals, int[][] expected)
    {
        var solution = new LC0056_MergeIntervals();

        var actual = solution.Merge(intervals);

        Assert.Equal(expected, actual);
    }
}
