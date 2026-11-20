namespace LeetCode.Benchmarks.Baselines;

/// <summary>
/// Community variant kept verbatim for comparison - do not tidy it up, or the
/// benchmark stops measuring the thing it was added to measure.
/// Keeps F(i-1) and F(i) in a two-slot array and picks the answer by parity.
/// </summary>
public sealed class LC0509_ParityArray
{
    public int Fib(int n)
    {
        int[] res = new int[2];
        res[0] = 0;
        res[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            if (i % 2 == 0)
            {
                res[0] += res[1];
            }
            else
            {
                res[1] += res[0];
            }
        }

        return res[n % 2];
    }
}
