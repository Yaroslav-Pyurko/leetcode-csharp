# CLAUDE.md

Guidance for Claude when working in this repository.

## What this is

LeetCode solutions in C#, targeting .NET 10. Three projects, wired together by
`LeetCode.slnx`:

| Path | What |
|---|---|
| `src/LeetCode` | the solutions (class library) |
| `tests/LeetCode.Tests` | xUnit tests, one file per solution |
| `benchmarks/LeetCode.Benchmarks` | BenchmarkDotNet console app |

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

Outstanding: the XML doc comment on `LC0509_FibonacciNumber` is ~25 lines of
prose explaining three approaches. It predates this rule and should move to a
README.

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

1. `Directory.Build.props` with `TreatWarningsAsErrors` and
   `GenerateDocumentationFile`. Most of items 5-8 below surface automatically
   once it exists, instead of being found by eye.
2. Root `README.md` with the problem index: number, name, difficulty, link,
   topic, complexity.
3. CI running `dotnet test` on push.
4. Root `.editorconfig`.
5. `LC2236` - dereferences `root.left` / `root.right` without a null check.
6. `LC0014` - `Array.Sort(strs)` mutates the caller's array;
   `IsNullOrWhiteSpace` should be `IsNullOrEmpty`.
7. `LC0642` - no namespace; `currentQuery += c` in a loop is O(n^2);
   `currNode` is non-nullable but assigned `null`.
8. `LC0021`, `LC0094`, `LC0144` - signatures declared non-nullable while the
   code and tests pass and return `null`.
9. `LC0200` - recursive DFS risks stack overflow on a dense grid and destroys
   the input grid; the iterative baseline in the benchmarks project shows the
   alternative.
10. `LC0094`, `LC0144` - `ref List<int>` is unnecessary for a reference type;
    private methods `inOrder` / `preOrder` should be PascalCase.
11. Test gaps: the three fast paths in `LC0088` are uncovered; `int.MinValue` is
    special-cased in `LC0007` but never tested.
