namespace LeetCode;

public class LC0056_MergeIntervals
{
    public int[][] Merge(int[][] intervals)
    {
        var n = intervals.Length;
        var hi = 0;
        for (var i = 0; i < n; i++)
        {
            if (intervals[i][1] > hi)
            {
                hi = intervals[i][1];
            }
        }

        var opens = new int[hi + 1];
        var closes = new int[hi + 1];
        for (var i = 0; i < n; i++)
        {
            var iv = intervals[i];
            opens[iv[0]]++;
            closes[iv[1]]++;
        }

        var cnt = 0;
        var depth = 0;
        var start = 0;
        for (var v = 0; v <= hi; v++)
        {
            if (opens[v] > 0)
            {
                if (depth == 0)
                {
                    start = v;
                }

                depth += opens[v];
            }

            if (closes[v] > 0)
            {
                depth -= closes[v];
                if (depth == 0)
                {
                    var iv = intervals[cnt++];
                    iv[0] = start;
                    iv[1] = v;
                }
            }
        }

        Array.Resize(ref intervals, cnt);
        return intervals;
    }
}
