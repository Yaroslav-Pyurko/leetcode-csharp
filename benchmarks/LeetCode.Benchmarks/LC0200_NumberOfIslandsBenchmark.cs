using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LeetCode.Benchmarks.Baselines;

namespace LeetCode.Benchmarks;

/// <summary>
/// Compares the recursive DFS solution in src with an iterative-stack variant.
///
/// Both implementations destroy the grid they traverse, so every invocation has
/// to work on a fresh copy. That copy is inside the measured region for both
/// benchmarks (it is the same cost for each), and <see cref="CloneOverhead"/>
/// reports what it is worth so it can be subtracted mentally from the results.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class LC0200_NumberOfIslandsBenchmark
{
    private const int Rows = 200;
    private const int Cols = 200;
    private const int Seed = 200_642;

    private char[][] _grid = [];

    /// <summary>
    /// Fraction of land cells.
    ///
    /// The site-percolation threshold for a square lattice is ~0.593: below it the
    /// grid breaks into many small islands, above it a single island spans almost
    /// everything and the recursive solution nests ~Rows * Cols calls deep, which
    /// overflows the default 1 MB stack. 0.55 is deliberately just under the
    /// threshold - raising it further will crash the RecursiveDfs benchmark, and
    /// that is exactly the difference the two implementations are here to show.
    /// </summary>
    [Params(0.30, 0.55)]
    public double LandDensity { get; set; }

    [GlobalSetup]
    public void Setup() => _grid = GridGenerator.Create(Rows, Cols, LandDensity, Seed);

    [Benchmark(Baseline = true, Description = "Recursive DFS (src)")]
    public int RecursiveDfs() =>
        new LC0200_NumberOfIslands().NumIslands(GridGenerator.Clone(_grid));

    [Benchmark(Description = "Iterative DFS (explicit stack)")]
    public int IterativeDfs() =>
        new LC0200_IterativeDfs().NumIslands(GridGenerator.Clone(_grid));

    [Benchmark(Description = "Grid clone only (overhead)")]
    public char[][] CloneOverhead() => GridGenerator.Clone(_grid);
}
