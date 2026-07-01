using LeetCode.Common;

namespace LeetCode.Tests
{
    public class LC0144_BinaryTreePreorderTraversalTests
    {
        [Fact]
        public void Example1()
        {
            var root = TreeBuilder.Build(1, null, 2, 3);

            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(root);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void EmptyTree()
        {
            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(null);

            Assert.Empty(result);
        }

        [Fact]
        public void SingleNode()
        {
            var root = TreeBuilder.Build(1);

            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(root);

            Assert.Equal(new[] { 1 }, result);
        }

        [Fact]
        public void CompleteBinaryTree()
        {
            //        1
            //      /   \
            //     2     3
            //    / \   / \
            //   4  5  6  7

            var root = TreeBuilder.Build(1, 2, 3, 4, 5, 6, 7);

            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(root);

            Assert.Equal(new[] { 1, 2, 4, 5, 3, 6, 7 }, result);
        }

        [Fact]
        public void LeftSkewedTree()
        {
            //     1
            //    /
            //   2
            //  /
            // 3

            var root = TreeBuilder.Build(1, 2, null, 3);

            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(root);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void RightSkewedTree()
        {
            // 1
            //  \
            //   2
            //    \
            //     3

            var root = TreeBuilder.Build(1, null, 2, null, 3);

            var result = new LC0144_BinaryTreePreorderTraversal().PreorderTraversal(root);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }
    }
}
