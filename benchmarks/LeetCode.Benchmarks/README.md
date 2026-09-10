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
