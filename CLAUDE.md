# CLAUDE.md

Guidance for Claude when working in this repository.

**Maintain this file as you go.** The owner has given standing permission to
edit it without asking: correct anything you find stale, record decisions and
conventions as they are agreed, and keep the backlog below current. He reviews
the diff before committing, so a wrong edit costs nothing but is still worth
avoiding. Do not let this file drift - a stale CLAUDE.md is worse than none.

## What this is

LeetCode solutions in C#, targeting .NET 10. Three projects, wired together by
`LeetCode.slnx`:

| Path | What |
|---|---|
| `src/LeetCode` | the solutions (class library) |
| `tests/LeetCode.Tests` | xUnit tests, one file per solution |
| `benchmarks/LeetCode.Benchmarks` | BenchmarkDotNet console app |

Two READMEs, cross-linked: the root one carries the problem index with per-
problem complexity, `benchmarks/LeetCode.Benchmarks/README.md` carries how to run
the benchmarks and what past runs measured. Prose that cannot live in a name
belongs in one of those two - see the comments section below.

## Commands

```bash
dotnet build
dotnet test
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *
```

`-c Release` on the benchmarks is mandatory. On a Debug build BenchmarkDotNet's
`OptimizationsValidator` aborts the run before measuring anything, and the error
names the referenced `LeetCode` assembly rather than the configuration, which
reads as a different problem than it is. See the benchmarks README.

## You cannot build or run anything here

The repository lives on the user's Windows machine and is reached through the
device bridge. The cloud container has **no .NET SDK**. So:

- Verify logic by reasoning, or by re-implementing the algorithm in Python and
  checking it against known values. Do that before claiming an algorithm works.
- Ask the user to run `dotnet build` / `dotnet test` and report back.
- Never state that code compiles or that tests pass. You have not seen either.

Files written back to the device must keep the repo's encoding: **UTF-8 with BOM
and CRLF** for `.cs` and `.csproj`, CRLF without BOM for `.md` and `.slnx`.

## Naming and layout

| Kind | Path | Type and namespace |
|---|---|---|
| Solution | `src/LeetCode/LC####_Problem.cs` | `LC####_Problem` in `LeetCode` |
| Test | `tests/LeetCode.Tests/LC####_ProblemTests.cs` | `LC####_ProblemTests` in `LeetCode.Tests` |
| Benchmark | `benchmarks/LeetCode.Benchmarks/LC####_ProblemBenchmark.cs` | `LeetCode.Benchmarks` |
| Shared helper | `src/LeetCode/Common/` | `LeetCode.Common` |

`ListNode` and `TreeNode` are LeetCode's own definitions and keep LeetCode's
lowercase field names (`val`, `next`, `left`, `right`). Do not "fix" those.

`LC0642_DesignSearchAutocompleteSystem` currently has no namespace at all. That
is a known defect, not a pattern to copy.

Style: block-scoped namespaces, Allman braces, four spaces. Test methods read
`Method_Scenario_ExpectedResult`; several older files predate that and are on the
backlog.

## Comments: the owner does not want them

His position, and it should be treated as the default: name things so that a
comment is never needed, because comments cost maintenance and rot silently.
In practice:

- Never write a comment that restates the line below it.
- A magic number becomes a named constant derived from its source of truth.
  `int.MaxValue / 10` beats `214_748_364` plus a comment explaining where
  `214_748_364` came from - the compiler folds it and it cannot drift.
- An explanation attached to a variable belongs *in the variable's name*.
- A block that needs a heading comment usually wants to be an extracted method
  with that heading as its name.

Facts that genuinely cannot live in a name - asymptotic complexity, external
constraints, why an obvious alternative was rejected - go in a README, or get
encoded as a test whose name carries the fact (`Fib_46_ReturnsLargestValueThatFitsInInt`
documents an overflow boundary and fails if it stops being true; a comment
cannot do that).

Where the prose goes. The root README carries the index table with a one-cell
approach label and the complexity per solution; the benchmarks README carries
measurements and anything about *how fast*. A longer-form Notes section was
added to the root README for LC0509 and then removed again, so at present the
repository keeps no per-problem prose beyond the table row - do not reintroduce
one without asking. The default remains: if it cannot live in a name, prefer a
test name that pins it, and otherwise keep it out.

If you do write an XML doc comment, remember it is XML: a bare `<` starts a tag
and makes the comment malformed (CS1570, currently invisible because
`GenerateDocumentationFile` is off). Prefer phrasing that avoids the character -
"the range [0, 30]" rather than "0 &lt;= n &lt;= 30".

## Benchmarks

- `Baselines/` holds implementations kept only for comparison, copied
  **verbatim** from wherever they came from. Never tidy them up - that changes
  what is being measured. They are not submissions and must not move to `src/`.
- Generated input uses a fixed seed. Never an unseeded `Random`.
- Construct the objects under test once, in fields, so the measurement is the
  method and not the allocation.
- At nanosecond scale, compare marginal cost across two input sizes rather than
  totals; the fixed per-call overhead cancels.
- Results and their interpretation live in
  `benchmarks/LeetCode.Benchmarks/README.md`, not in code comments.

## Agreed backlog

Reviewed and accepted, not yet done. Roughly in order of leverage:

1. `Directory.Build.props` holding the properties currently duplicated across
   all three csproj files (`TargetFramework`, `ImplicitUsings`, `Nullable`),
   plus `TreatWarningsAsErrors`. Note that `Nullable` is *already* enabled, so
   items 4-6 below are warnings the compiler emits today and nobody reads;
   `TreatWarningsAsErrors` adds no analysis, it only stops the ignoring.
   Expect roughly 8-12 diagnostics across 5-6 files - a bounded job.

   Two traps: adding `GenerateDocumentationFile` also turns on **CS1591**
   ("missing XML comment for publicly visible member"), which would demand a doc
   comment on every public member - the opposite of the rule above, and an
   instant build failure under `TreatWarningsAsErrors`. Pair it with
   `<NoWarn>$(NoWarn);CS1591</NoWarn>`. And do not add `EnforceCodeStyleInBuild`
   or `AnalysisLevel` at the same time; those raise hundreds of IDE#### style
   diagnostics and are a separate decision.

   Land it as two commits: deduplication first (no behaviour change), the
   strictness flag second, so it can be reverted on its own.
2. CI running `dotnet test` on push. The remote is
   `github.com/Yaroslav-Pyurko/leetcode-csharp`, branch `main`, public, so
   Actions minutes are free. This is what would have caught the broken
   `benchmark.cs` that sat in the test project unnoticed for weeks. Do not run
   benchmarks in CI - shared runners produce meaningless nanosecond numbers.
3. Root `.editorconfig`.
4. `LC2236` - dereferences `root.left` / `root.right` without a null check.
5. `LC0642` - no namespace; `currentQuery += c` in a loop is O(n^2);
   `currNode` is non-nullable but assigned `null`.
6. `LC0021`, `LC0094`, `LC0144` - signatures declared non-nullable while the
   code and tests pass and return `null`.
7. `LC0200` - recursive DFS risks stack overflow on a dense grid and destroys
   the input grid; the iterative baseline in the benchmarks project shows the
   alternative.
8. `LC0094`, `LC0144` - `ref List<int>` is unnecessary for a reference type;
   private methods `inOrder` / `preOrder` should be PascalCase.
9. Test gaps: the three fast paths in `LC0088` are uncovered; `int.MinValue` is
    special-cased in `LC0007` but never tested.
10. `LC0200_NumberOfIslandsBenchmark` has never been run; its section in the
    benchmarks README is still a placeholder.

## Done

- Benchmarks split out of the test project into
  `benchmarks/LeetCode.Benchmarks` (they had been silently breaking the build).
- Root `README.md` with the problem index, complexity per solution, build
  commands, and cross-links to the benchmarks README.
- `LC0026` rewritten to two pointers, `LC0217` to a `HashSet`, `LC0509` to a
  rolling pair - all three were asymptotically worse than the problem intended.
- `LC0509` benchmarked against two community variants; results and analysis are
  in the benchmarks README.
- `LC0014` rewritten from sort-then-compare to a vertical scan: it no longer
  reorders the caller's array, no longer discards whitespace-only elements, and
  drops from O(m * n log n) to O(n * m). Tests now pin both former defects.
