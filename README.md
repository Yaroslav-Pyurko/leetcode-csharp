# leetcode-csharp

LeetCode solutions in C# on .NET 10. One class per problem in `src/LeetCode`,
one xUnit test file per solution in `tests/LeetCode.Tests`.

**20 solved:** 15 Easy, 4 Medium, 1 Hard.

## Layout

| Path | Contents |
|---|---|
| `src/LeetCode` | solutions, one class per problem |
| `tests/LeetCode.Tests` | xUnit tests, one file per solution |
| `benchmarks/LeetCode.Benchmarks` | BenchmarkDotNet comparisons - [README](benchmarks/LeetCode.Benchmarks/README.md) |

## Building and running

```bash
dotnet build
dotnet test
dotnet run -c Release --project benchmarks/LeetCode.Benchmarks -- --filter *
```

`-c Release` is required for the benchmarks: on a Debug build the run is rejected
before anything is measured. How to read the output, and the measurements taken
so far, are in the [benchmarks README](benchmarks/LeetCode.Benchmarks/README.md).

## Solutions

Complexity below describes the implementation in this repository, not the
theoretical optimum. `n` is the input size, `m` the second input or the string
length, `h` the tree height, `r` and `c` the grid dimensions, `k` the word or
candidate count, `L` the average sentence length.

| # | Problem | Difficulty | Approach | Time | Space | Code |
|---:|---|---|---|---|---|---|
| 1 | [Two Sum](https://leetcode.com/problems/two-sum/) | Easy | Hash map | O(n) | O(n) | [src](src/LeetCode/LC0001_TwoSum.cs) &middot; [tests](tests/LeetCode.Tests/LC0001_TwoSumTests.cs) |
| 7 | [Reverse Integer](https://leetcode.com/problems/reverse-integer/) | Medium | Math, overflow | O(log x) | O(1) | [src](src/LeetCode/LC0007_ReverseInteger.cs) &middot; [tests](tests/LeetCode.Tests/LC0007_ReverseIntegerTests.cs) |
| 9 | [Palindrome Number](https://leetcode.com/problems/palindrome-number/) | Easy | Math | O(log x) | O(1) | [src](src/LeetCode/LC0009_PalindromeNumber.cs) &middot; [tests](tests/LeetCode.Tests/LC0009_PalindromeNumberTests.cs) |
| 14 | [Longest Common Prefix](https://leetcode.com/problems/longest-common-prefix/) | Easy | String, sorting | O(m &middot; n log n) | O(m) | [src](src/LeetCode/LC0014_LongestCommonPrefix.cs) &middot; [tests](tests/LeetCode.Tests/LC0014_LongestCommonPrefixTests.cs) |
| 20 | [Valid Parentheses](https://leetcode.com/problems/valid-parentheses/) | Easy | Stack | O(n) | O(n) | [src](src/LeetCode/LC0020_ValidParentheses.cs) &middot; [tests](tests/LeetCode.Tests/LC0020_ValidParenthesesTests.cs) |
| 21 | [Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/) | Easy | Linked list, dummy node | O(n + m) | O(1) | [src](src/LeetCode/LC0021_MergeTwoSortedLists.cs) &middot; [tests](tests/LeetCode.Tests/LC0021_MergeTwoSortedListsTests.cs) |
| 26 | [Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/) | Easy | Two pointers | O(n) | O(1) | [src](src/LeetCode/LC0026_RemoveDuplicatesFromSortedArray.cs) &middot; [tests](tests/LeetCode.Tests/LC0026_RemoveDuplicatesFromSortedArrayTests.cs) |
| 49 | [Group Anagrams](https://leetcode.com/problems/group-anagrams/) | Medium | Hash map, counting sort key | O(n &middot; k) | O(n &middot; k) | [src](src/LeetCode/LC0049_GroupAnagrams.cs) &middot; [tests](tests/LeetCode.Tests/LC0049_GroupAnagramsTests.cs) |
| 88 | [Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/) | Easy | Two pointers, merge from the back | O(n + m) | O(1) | [src](src/LeetCode/LC0088_MergeSortedArray.cs) &middot; [tests](tests/LeetCode.Tests/LC0088_MergeSortedArrayTests.cs) |
| 94 | [Binary Tree Inorder Traversal](https://leetcode.com/problems/binary-tree-inorder-traversal/) | Easy | Tree, recursive DFS | O(n) | O(h) | [src](src/LeetCode/LC0094_BinaryTreeInorderTraversal.cs) &middot; [tests](tests/LeetCode.Tests/LC0094_BinaryTreeInorderTraversalTests.cs) |
| 144 | [Binary Tree Preorder Traversal](https://leetcode.com/problems/binary-tree-preorder-traversal/) | Easy | Tree, recursive DFS | O(n) | O(h) | [src](src/LeetCode/LC0144_BinaryTreePreorderTraversal.cs) &middot; [tests](tests/LeetCode.Tests/LC0144_BinaryTreePreorderTraversalTests.cs) |
| 198 | [House Robber](https://leetcode.com/problems/house-robber/) | Medium | Dynamic programming | O(n) | O(1) | [src](src/LeetCode/LC0198_HouseRobber.cs) &middot; [tests](tests/LeetCode.Tests/LC0198_HouseRobberTests.cs) |
| 200 | [Number of Islands](https://leetcode.com/problems/number-of-islands/) | Medium | Grid, DFS flood fill | O(r &middot; c) | O(r &middot; c) | [src](src/LeetCode/LC0200_NumberOfIslands.cs) &middot; [tests](tests/LeetCode.Tests/LC0200_NumberOfIslandsTests.cs) |
| 217 | [Contains Duplicate](https://leetcode.com/problems/contains-duplicate/) | Easy | Hash set | O(n) | O(n) | [src](src/LeetCode/LC0217_ContainsDuplicate.cs) &middot; [tests](tests/LeetCode.Tests/LC0217_ContainsDuplicateTests.cs) |
| 242 | [Valid Anagram](https://leetcode.com/problems/valid-anagram/) | Easy | Counting, fixed alphabet | O(n) | O(1) | [src](src/LeetCode/LC0242_ValidAnagram.cs) &middot; [tests](tests/LeetCode.Tests/LC0242_ValidAnagramTests.cs) |
| 509 | [Fibonacci Number](https://leetcode.com/problems/fibonacci-number/) | Easy | Dynamic programming, rolling pair | O(n) | O(1) | [src](src/LeetCode/LC0509_FibonacciNumber.cs) &middot; [tests](tests/LeetCode.Tests/LC0509_FibonacciNumberTests.cs) |
| 642 | [Design Search Autocomplete System](https://leetcode.com/problems/design-search-autocomplete-system/) | Hard | Trie, design | O(k log k) per input | O(n &middot; L) | [src](src/LeetCode/LC0642_DesignSearchAutocompleteSystem.cs) &middot; [tests](tests/LeetCode.Tests/LC0642_DesignSearchAutocompleteSystemTests.cs) |
| 859 | [Buddy Strings](https://leetcode.com/problems/buddy-strings/) | Easy | String | O(n) | O(1) | [src](src/LeetCode/LC0859_BuddyStrings.cs) &middot; [tests](tests/LeetCode.Tests/LC0859_BuddyStringsTests.cs) |
| 2235 | [Add Two Integers](https://leetcode.com/problems/add-two-integers/) | Easy | Math | O(1) | O(1) | [src](src/LeetCode/LC2235_AddTwoIntegers.cs) &middot; [tests](tests/LeetCode.Tests/LC2235_AddTwoIntegersTests.cs) |
| 2236 | [Root Equals Sum of Children](https://leetcode.com/problems/root-equals-sum-of-children/) | Easy | Tree | O(1) | O(1) | [src](src/LeetCode/LC2236_RootEqualsSumOfChildren.cs) &middot; [tests](tests/LeetCode.Tests/LC2236_RootEqualsSumOfChildrenTests.cs) |
