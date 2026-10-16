namespace LeetCode;

public class LC0056_MergeIntervals
{
    private const int MaxCoordinate = 10_000;
    private const int NoIntervalStartsHere = -1;

    public int[][] Merge(int[][] intervals)
    {
        var furthestEndByStart = new int[MaxCoordinate + 1];
        Array.Fill(furthestEndByStart, NoIntervalStartsHere);

        foreach (var interval in intervals)
        {
            var start = interval[0];
            furthestEndByStart[start] = Math.Max(furthestEndByStart[start], interval[1]);
        }

        var merged = new List<int[]>();

        for (var start = 0; start <= MaxCoordinate; start++)
        {
            var end = furthestEndByStart[start];
            if (end == NoIntervalStartsHere)
            {
                continue;
            }

            var overlapsLast = merged.Count > 0 && start <= merged[^1][1];
            if (overlapsLast)
            {
                merged[^1][1] = Math.Max(merged[^1][1], end);
            }
            else
            {
                merged.Add([start, end]);
            }
        }

        return merged.ToArray();
    }
}
