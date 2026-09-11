# LeetCode.Benchmarks

Part of [leetcode-csharp](../../README.md) - the root README holds the problem
index.

Micro-benchmarks for the solutions in `src/LeetCode`, powered by
[BenchmarkDotNet](https://benchmarkdotnet.org/).

This is a separate console project on purpose: BenchmarkDotNet needs a Release
build and its own host process, neither of which it gets when it lives inside a
unit-test project.

## Running

```bash
# everything, non-interactive
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *

# one problem
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *Fibonacci*
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *NumberOfIslands*

# interactive menu (pick from a numbered list)
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks

# add the generated machine code to the report
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *Fibonacci* --disasm
```

From Visual Studio: set **LeetCode.Benchmarks** as the startup project, switch
the configuration to **Release**, and run without the debugger (Ctrl+F5).
Attaching the debugger perturbs exactly what is being measured.

Reports (markdown, csv, html) are written to `BenchmarkDotNet.Artifacts/` in the
working directory. That folder is gitignored - copy anything worth keeping into
the Results section below.

### `-c Release` is not optional

On a Debug build the run aborts before measuring anything:

```
* Assembly LeetCode.Benchmarks which defines benchmarks references non-optimized LeetCode
  If you own this dependency, please, build it in RELEASE.
```

That is the validator doing its job, not a bug. A Debug JIT skips inlining, loop
unrolling and dead-code elimination, so the numbers would describe the JIT rather
than the algorithm. There is a `ConfigOptions.DisableOptimizationsValidator`
escape hatch - do not use it; it silences the message without making the
measurements mean anything.

### Reading the output

- **`Allocated`** - bytes on the managed heap per invocation. A dash means zero,
  which is not the same as "no memory used": on net10.0 escape analysis can keep
  a short-lived array on the stack, where it still costs load/store traffic.
- **`Ratio` / `RatioSD`** - relative to the method marked `Baseline = true`. If
  `Ratio` sits inside roughly 1 +/- 2 * `RatioSD`, treat the two as tied.
- **Warnings printed under the table** - read them before the numbers. A note
  like "the method duration is indistinguishable from the empty method duration"
  means the measurement hit the resolution floor and the comparison is void.
- **Nanosecond-scale results** - compare the *marginal* cost instead of the
  totals. Two runs with different input sizes let you subtract the fixed
  per-call overhead; see the LC0509 note below for a worked example.

## What is measured

| Benchmark | Compares |
|---|---|
| `LC0509_FibonacciNumberBenchmark` | the rolling-pair loop in src vs two community variants |
| `LC0200_NumberOfIslandsBenchmark` | recursive DFS in src vs an explicit-stack DFS |

## Results

Numbers are from one machine (Acer Swift 14, Windows, net10.0) and are only
meaningful as ratios. Re-measure before trusting them on other hardware.

### LC0509 - Fibonacci, 2026-09

Background - why the solution is a bottom-up loop rather than recursion, and how
tabulation differs from memoisation - is in the
[root README notes](../../README.md#509---fibonacci-number). What follows is the
narrower question: three formulations that are all O(n), and why they are not
equally fast.

| Method | N | Mean | Ratio | Allocated |
|---|---:|---:|---:|---:|
| Rolling pair (src) | 30 | 9.151 ns | 1.00 | - |
| Three variables | 30 | 10.747 ns | 1.17 | - |
| Parity-indexed array | 30 | 31.747 ns | 3.47 | - |
| Rolling pair (src) | 46 | 13.946 ns | 1.00 | - |
| Three variables | 46 | 16.129 ns | 1.16 | - |
| Parity-indexed array | 46 | 53.169 ns | 3.81 | - |

The two input sizes differ by exactly 16 loop iterations, so subtracting them
cancels the per-call overhead and gives the cost of one iteration:

| | ns per iteration |
|---|---:|
| Rolling pair (src) | 0.300 |
| Three variables | 0.336 |
| Parity-indexed array | 1.339 |

Findings:

- The rolling pair costs roughly one cycle per iteration, which is the latency of
  the addition itself. The loop-carried dependency makes that the floor - no
  formulation of this recurrence can beat it.
- The array variant allocates nothing (escape analysis keeps `int[2]` off the
  heap on net10.0) yet is still ~4.5x more expensive per iteration. The likely
  cause is *where the dependency chain runs*: through a stack slot rather than a
  register, so each iteration waits on store-to-load forwarding. ~5.4 cycles fits
  that explanation, but it has not been confirmed against `--disasm`.
- The branch in the array variant is not the problem: `i % 2` alternates
  perfectly and predicts at ~100%.
- The three-variable variant is consistently 16-17% slower than the rolling pair,
  well outside the error bars. The mechanism is unexplained; `--disasm` would
  settle it.

Perspective: the whole spread is 23 nanoseconds per call. All three submit to
LeetCode as "0 ms". This benchmark exists to answer *why*, not to pick a winner.

### LC0200 - Number of Islands

Not run yet.

## Conventions

- One benchmark class per problem, named `LC####_<Problem>Benchmark`, mirroring
  `src/LeetCode/LC####_<Problem>.cs`.
- Alternative implementations that exist only for comparison go in `Baselines/`.
  They are not LeetCode submissions and must not leak into `src/`.
  **Copy them verbatim.** Tidying a baseline changes what is being measured.
- Inputs are generated from a fixed seed so that two runs compare the same data.
  Never use an unseeded `Random` here.
- Solutions that mutate their input (LC0200 does) get a fresh copy per
  invocation, and the copy cost is reported as its own benchmark so it can be
  subtracted.
- Construct the objects under test once, in fields, so the measurement is the
  method and not the allocation.

## Notes

A table row states what a solution does. These are the few cases where *why* is
worth a paragraph, and where that reasoning has nowhere else to live - the code
itself carries no comments by design.

### 509 - Fibonacci Number

Three ways to evaluate F(n), in the order they tend to get discovered:

1. **Plain recursion** - `F(n) = F(n-1) + F(n-2)`, straight off the definition.
   Beautiful, and O(phi^n): the two calls re-derive overlapping subtrees from
   scratch, so F(30) alone expands to roughly 2.7 million calls.
2. **Memoisation (top-down)** - keep the recursion, but cache each F(i) so it is
   computed once. O(n) time, at the cost of an O(n) table plus n stack frames.
3. **Tabulation (bottom-up)** - fill the same table forwards from F(0), so
   nothing recurses. And since F(i) reads only F(i-1) and F(i-2), the table never
   needs more than its last two entries: it collapses into two variables. O(n)
   time, O(1) space. This is what the solution here does, and
   [198 House Robber](src/LeetCode/LC0198_HouseRobber.cs) uses the identical
   two-variable pattern.

Worth naming precisely, because the terms get swapped: (3) is **not**
memoisation. Nothing is cached and nothing recurses. It is the same
dynamic-programming recurrence evaluated in the opposite direction, and that
direction is exactly what lets the storage shrink to a constant.

`int` holds Fibonacci numbers up to F(46) = 1,836,311,903; F(47) overflows.
LeetCode constrains n to [0, 30], so it never bites here - and the boundary is
pinned by a test rather than described in a comment.

Rolling pair, memoisation and a two-slot array are not equally fast even though
all three are O(n). Measured comparison, and why the array formulation loses:
[benchmarks README](benchmarks/LeetCode.Benchmarks/README.md#lc0509---fibonacci-2026-09).
