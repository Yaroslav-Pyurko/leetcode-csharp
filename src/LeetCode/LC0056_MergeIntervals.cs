namespace LeetCode;

public class LC0056_MergeIntervals
{
    public int[][] Merge(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0)
        {
            return new int[0][];
        }

        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        var merged = new List<int[]>();

        foreach (var interval in intervals)
        {
            var noOverlap = merged.Count == 0 || merged[^1][1] < interval[0];
            var overlap = !noOverlap;
            if (overlap)
            {
                merged[^1][1] = Math.Max(merged[^1][1], interval[1]);
            }
            else
            {
                merged.Add(new int[] { interval[0], interval[1] });
            }
        }

        return merged.ToArray();
    }
}
