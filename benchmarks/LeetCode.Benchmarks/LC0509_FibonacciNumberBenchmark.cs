using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LeetCode.Benchmarks.Baselines;

namespace LeetCode.Benchmarks;

/// <summary>
/// Three ways to run the same O(n) Fibonacci recurrence.
///
/// N = 30 is the LeetCode upper bound; N = 46 is the largest value that still
/// fits in int, and it triples the loop count so per-iteration differences have
/// room to show. Each solution is instantiated once, in a field, so the numbers
/// measure Fib and not object construction.
///
/// MemoryDiagnoser is here for one specific question: whether the int[2] in
/// LC0509_ParityArray reaches the heap. On net10.0 escape analysis should keep
/// it on the stack, which would show as 0 B allocated.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class LC0509_FibonacciNumberBenchmark
{
    private readonly LC0509_FibonacciNumber _rollingPair = new();
    private readonly LC0509_ParityArray _parityArray = new();
    private readonly LC0509_ThreeVariables _threeVariables = new();

    [Params(30, 46)]
    public int N { get; set; }

    [Benchmark(Baseline = true, Description = "Rolling pair (src)")]
    public int RollingPair() => _rollingPair.Fib(N);

    [Benchmark(Description = "Parity-indexed array")]
    public int ParityArray() => _parityArray.Fib(N);

    [Benchmark(Description = "Three variables")]
    public int ThreeVariables() => _threeVariables.Fib(N);
}
