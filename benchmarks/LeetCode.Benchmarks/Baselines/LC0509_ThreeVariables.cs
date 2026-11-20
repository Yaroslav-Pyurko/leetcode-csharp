namespace LeetCode.Benchmarks.Baselines;

/// <summary>
/// Community variant kept verbatim for comparison - do not tidy it up, or the
/// benchmark stops measuring the thing it was added to measure.
/// Same rolling-pair recurrence as the solution in src, written with a third
/// carry variable and counting n itself down instead of a separate index.
/// </summary>
public sealed class LC0509_ThreeVariables
{
    public int Fib(int n)
    {
        if (n == 0) return 0;

        var a = 0;
        var b = 0;
        var c = 1;

        while (n > 1)
        {
            a = b;
            b = c;
            c += a;
            n--;
        }

        return c;
    }
}
