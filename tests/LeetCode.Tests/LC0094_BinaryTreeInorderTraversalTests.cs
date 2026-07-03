using LeetCode.Common;

namespace LeetCode.Tests
{
    public class LC0094_BinaryTreeInorderTraversalTests
    {
        [Fact]
        public void Example1()
        {
            var root = TreeBuilder.Build(1, null, 2, 3);

            var result = new LC0094_BinaryTreeInorderTraversal().InorderTraversal(root);

            Assert.Equal(new[] { 1, 3, 2 }, result);
        }

        [Fact]
        public void EmptyTree_ReturnsEmptyList()
        {
            var result = new LC0094_BinaryTreeInorderTraversal().InorderTraversal(null);

            Assert.Empty(result);
        }

        [Fact]
        public void SingleNode_ReturnsSingleElement()
        {
            var root = TreeBuilder.Build(1);

            var result = new LC0094_BinaryTreeInorderTraversal().InorderTraversal(root);

            Assert.Equal(new[] { 1 }, result);
        }

        [Fact]
        public void CompleteTree_ReturnsInorderSequence()
        {
            var root = TreeBuilder.Build(1, 2, 3, 4, 5, 6, 7);

            var result = new LC0094_BinaryTreeInorderTraversal().InorderTraversal(root);

            Assert.Equal(new[] { 4, 2, 5, 1, 6, 3, 7 }, result);
        }
    }
}