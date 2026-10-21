namespace LeetCode;

public class LC0056_MergeIntervals
{
    public int[][] Merge(int[][] intervals)
    {
        var maxEnd = 0;
        foreach (var interval in intervals)
        {
            maxEnd = Math.Max(maxEnd, interval[1]);
        }

        var opensAt = new int[maxEnd + 1];
        var closesAt = new int[maxEnd + 1];
        foreach (var interval in intervals)
        {
            opensAt[interval[0]]++;
            closesAt[interval[1]]++;
        }

        var mergedCount = 0;
        var openCount = 0;
        var mergedStart = 0;
        for (var point = 0; point <= maxEnd; point++)
        {
            if (openCount == 0 && opensAt[point] > 0)
            {
                mergedStart = point;
            }

            openCount += opensAt[point] - closesAt[point];

            if (openCount == 0 && closesAt[point] > 0)
            {
                var merged = intervals[mergedCount++];
                merged[0] = mergedStart;
                merged[1] = point;
            }
        }

        Array.Resize(ref intervals, mergedCount);
        return intervals;
    }
}
