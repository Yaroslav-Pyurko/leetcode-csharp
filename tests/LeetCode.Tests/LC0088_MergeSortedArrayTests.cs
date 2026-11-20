namespace LeetCode.Tests;

public class LC0088_MergeSortedArrayTests
{
    [Theory]
    [InlineData(
        new int[] { 1, 2, 3, 0, 0, 0 }, 
        3,
        new int[] { 2, 5, 6 }, 
        3,
        new int[] { 1, 2, 2, 3, 5, 6 })]
    [InlineData(
        new int[] { 1 },
        1,
        new int[] { },
        0,
        new int[] { 1 })]
    [InlineData(
        new int[] { 0 },
        0,
        new int[] { 1 },
        1,
        new int[] { 1 })]
    public void Merge_ReturnsExpectedResult(int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        var solution = new LC0088_MergeSortedArray();

        solution.Merge(nums1, m, nums2, n);
        var actual = nums1;

        Assert.Equal(expected, actual);
    }
}
