using LeetCode.Common;

namespace LeetCode.Tests
{
    public class LC0021_MergeTwoSortedListsTests
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 4 }, new int[] { 1, 3, 4 }, new int[] { 1, 1, 2, 3, 4, 4 })]
        [InlineData(new int[] { }, new int[] { }, new int[] { })]
        [InlineData(new int[] { }, new int[] { 0 }, new int[] { 0 })]
        [InlineData(
            new int[] { 1, 2, 3, 4 }, 
            new int[] { 5, 6, 7, 8 }, 
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void MergeTwoLists_ReturnsExpectedResult(int[] list1, int[] list2, int[] expected)
        {
            var listNode1 = LinkedListBuilder.ToListNode(list1);
            var listNode2 = LinkedListBuilder.ToListNode(list2);
            var solution = new LC0021_MergeTwoSortedLists();

            var actual = solution.MergeTwoLists(listNode1, listNode2);

            Assert.Equal(expected, LinkedListBuilder.ToArray(actual));
        }
    }
}
