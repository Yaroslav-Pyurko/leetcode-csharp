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

The owner switches git branches between turns, so the working tree can change
under you with no message in the conversation. A file you wrote earlier may come
back looking reverted - that is a checkout, not someone rejecting the edit, and
re-applying it blindly would write one branch's content onto another. Before
editing any file you have not read *this turn*, re-read it from the device and
diff against your copy. `.git/HEAD` names the current branch if it matters which
one you are about to change.

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

Style is defined by the root `.editorconfig`: file-scoped namespaces (existing
files are still block-scoped, see the backlog), Allman braces, four spaces,
`_camelCase` private fields, `s_camelCase` private static fields, `var` everywhere
(the owner's preference). `Baselines/` is marked `generated_code` there, so style
rules never flag the verbatim copies. Test methods read
`Method_Scenario_ExpectedResult`; several older files predate that and are on the
backlog.

One test class per solution, and inside it prefer **a single `[Theory]` with
`InlineData` rows** over several methods. Do not split a group of cases into its
own method just to give the group a name - if the signature is the same, it is
the same test, and the rows say what the name would have. A second method earns
its place only when it genuinely needs a different signature or fixture. An
assertion that holds for every row (for example "the input array is not
modified") belongs inside the one theory, where it is checked on every row rather
than on one hand-picked case.

## Write to the LeetCode constraints, and no further

Every solution targets one problem with a published Constraints section. Code
that handles inputs the constraints rule out is dead weight: it can never run, it
still has to be read and maintained, and it hides which guarantees the algorithm
actually relies on. Read the constraints before adding a guard, and delete guards
that the constraints make unreachable.

LC0014 is the worked example. Its constraints are `1 <= strs.length <= 200`,
`0 <= strs[i].length <= 200`, lowercase letters only when non-empty. So:

- `if (strs.Length == 0)` was removed - the array is never empty, `strs[0]` is
  always safe.
- Test rows with whitespace characters were removed - out of the alphabet.
- Test rows with empty strings stayed - `0 <= strs[i].length` allows them.

The same applies to test data: a row exercising input the constraints forbid
proves nothing about the submitted solution.

This is a rule about *unreachable* code, not about correctness: keep any check the
constraints permit to matter.

The rule was applied to LC0014 all the way down. An earlier version of its theory
also asserted that the input array came back unreordered - a regression pin for a
real defect, the old `Array.Sort(strs)`. The owner removed it too, on the grounds
that LeetCode does not require it and that nothing in this repository calls the
method except the test itself. So the precedent is strict: beyond-spec checks go,
even ones guarding a defect that actually happened. A rewrite reintroducing a
sort would be a deliberate act, not a silent regression.

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

Where the prose goes. The root README carries the index table - a one-cell
approach label and the complexity per solution; the benchmarks README carries
measurements and anything about *how fast*. A longer-form Notes section was once
added to the root README for LC0509 and then removed again, so the repository
currently keeps no per-problem prose beyond the table row; do not reintroduce one
without asking. The default stands: if a fact cannot live in a name, prefer a
test name that pins it, and otherwise leave it out.

If you do write an XML doc comment, remember it is XML: a bare `<` starts a tag
and makes the comment malformed (CS1570, currently invisible because
`GenerateDocumentationFile` is off). Prefer phrasing that avoids the character -
"the range [0, 30]" rather than "0 &lt;= n &lt;= 30".

Before adding or bumping a package, check its nuget.org page for a deprecation
notice. The `xunit` 2.x reference in this repository sat there for months while
being formally deprecated, and the original review listed the package versions
without ever looking.

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

1. Convert every file to file-scoped namespaces (IDE0161, one "Fix all in
   Solution" in Visual Studio). After that, decide on `EnforceCodeStyleInBuild`:
   until it is on, the `.editorconfig` severities show only in the IDE and the
   build ignores them. With `TreatWarningsAsErrors` every style `warning` would
   then fail the build, so land it only once the IDE shows none.

   Counted on 2026-10-02, the outstanding style debt is about 36 items and
   almost all of it is mechanical:

   - IDE0161 file-scoped namespace: 21 of 24 files (20 block-scoped, plus
     `LC0642` which has no namespace at all).
   - IDE0011 braces: 11 single-statement `if` bodies, in `LC0007`, `LC0026`,
     `LC0198`, `LC0242`, `LC0859` and `TreeBuilder`. Four of them disappear by
     themselves once the dead guards in item 2 are deleted.
   - IDE1006 private field naming and IDE0044 readonly: all in `LC0642` -
     `root`, `currNode` and `currentQuery` need the `_` prefix, and `root` is
     never reassigned. Doing item 3 clears these too.
2. Audit the guards that the constraints make unreachable, per the rule above.
   Verified against the published constraints, all four are dead code:

   - `LC0198`: `nums == null || nums.Length == 0` and also
     `nums.Length == 1` - constraint is `1 <= nums.length <= 100`, and the
     rolling-pair loop already returns `nums[0]` correctly for one element, so
     both lines go.
   - `LC0020`: `s.Length == 0` - constraint is `1 <= s.length <= 10^4`, and the
     final `stack.Count == 0` would return `true` for it anyway.
   - `LC0026`: `nums.Length == 0` - constraint is `1 <= nums.length <= 3*10^4`.
   - `LC0200`: `grid == null || grid.Length == 0 || grid[0].Length == 0` -
     constraint is `1 <= m, n <= 300`.

   **Do not touch `LC0007`'s `if (x == int.MinValue) return 0;`** - it looks like
   the same pattern but its constraint is `-2^31 <= x <= 2^31 - 1`, so
   `int.MinValue` is a legal input and the guard is load-bearing.

   Unrelated to constraints but in the same sweep: `LC0009` opens with
   `if (0 <= x && x <= 9) return true;`, which the main path already handles
   correctly for 0-9.
3. Migrate the test project from xUnit v2 to v3. The `xunit` 2.x meta-package is
   formally **deprecated** on nuget.org - "legacy and no longer maintained",
   security fixes only - which is why Visual Studio shows it struck through.
   Nothing is broken, so this is not urgent, but v2 gets no further work.

   The smaller of the two migration paths keeps VSTest and so keeps
   `dotnet test`, the CI workflow and `coverlet.collector` working unchanged:

   - `xunit` 2.9.3 -> `xunit.v3` (its version numbering is confusing: the
     package is named v3 but the current release is 4.0.1).
   - `xunit.runner.visualstudio` needs >= 3.0.0; 3.1.4 already qualifies.
   - Keep `Microsoft.NET.Test.Sdk` and `coverlet.collector`.
   - Add `<OutputType>Exe</OutputType>` - v3 test projects are self-executing,
     and `Library` is the default.

   The other path swaps VSTest for Microsoft Testing Platform (`xunit.v3.mtp-v2`,
   dropping Test.Sdk and runner.visualstudio). Do not take it: coverlet.collector
   is a VSTest data collector and would stop working.

   **Expect the build to break on the first attempt.** `TreatWarningsAsErrors` is
   on, and the v3 analysers are stricter. The likely offenders are
   `LC0200_NumberOfIslandsTests` and `LC0642_DesignSearchAutocompleteSystemTests`,
   whose `MemberData` sources return untyped `IEnumerable<object[]>`; newer
   xunit.analyzers flag that (xUnit1042-family) and the fix is `TheoryData<...>`.
   Land the migration as its own commit so it can be reverted cleanly.
4. `LC0642` - no namespace, so the class sits in the global one while every
   other solution is in `LeetCode`; and `currentQuery += c` inside `Input` is
   O(n^2) string building where a `StringBuilder` belongs.
5. `LC0200` - recursive DFS risks stack overflow on a dense grid and destroys
   the input grid; the iterative baseline in the benchmarks project shows the
   alternative.
6. Test gaps: the three fast paths in `LC0088` are uncovered; `int.MinValue` is
   special-cased in `LC0007` but never tested.
7. `LC0200_NumberOfIslandsBenchmark` has never been run; its section in the
   benchmarks README is still a placeholder.

## Done

- `Directory.Build.props` at the root carries `TargetFramework`,
  `ImplicitUsings`, `Nullable`, `IsPackable` and `TreatWarningsAsErrors` for all
  three projects; the csproj files keep only what is genuinely theirs, and
  `src/LeetCode/LeetCode.csproj` is now empty apart from a pointer comment.
  Zero warnings is an enforced invariant rather than a snapshot.
- Root `.editorconfig` with 13 IDE rules raised to `warning`. Note these are
  IDE-only until `EnforceCodeStyleInBuild` lands - see item 1.
- Root `.editorconfig` with the full code style, merged from the former
  `my_defaul_style.editorconfig` and modernised (file-scoped namespaces, C# 12-13
  options, `_camelCase` / `s_camelCase` fields). Charset follows the repo:
  UTF-8 with BOM for `.cs` and `.csproj`, without BOM for everything else.
- CI on GitHub Actions: `.github/workflows/ci.yml` restores, builds and tests
  on every push and pull request to `main`. Benchmarks are deliberately not run
  there - shared runners produce meaningless nanosecond numbers.
- Benchmarks split out of the test project into
  `benchmarks/LeetCode.Benchmarks` (they had been silently breaking the build).
- Root `README.md` with the problem index, complexity per solution, build
  commands, and cross-links to the benchmarks README.
- `LC0026` rewritten to two pointers, `LC0217` to a `HashSet`, `LC0509` to a
  rolling pair - all three were asymptotically worse than the problem intended.
- `LC0509` benchmarked against two community variants; results and analysis are
  in the benchmarks README.
- Nullable signatures corrected across `LC0021`, `LC0094`, `LC0144`, `LC2236`
  and `LC0642`, taking the build from 21 warnings to zero. Each change follows
  the problem's constraints rather than silencing the analyser: LC0094 and
  LC0144 accept `[0, 100]` nodes and LC0021 accepts `[0, 50]`, so `null` is a
  legal argument and the parameters say so; LC2236 has exactly three nodes, so
  its children use `!`. Fixing the two public tree signatures alone cleared 12
  of the 21. The same pass dropped the needless `ref List<int>` and renamed
  `inOrder` / `preOrder` to PascalCase.
- `LC0014` rewritten from sort-then-compare to a vertical scan: it no longer
  reorders the caller's array, no longer discards whitespace-only elements, and
  drops from O(m * n log n) to O(n * m). The solution and its test rows were then
  trimmed to exactly what the LeetCode constraints allow.
