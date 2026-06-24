namespace LeetCode
{
    public class LC0094_BinaryTreeInorderTraversal
    {
        IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            inOrder(root, ref result);
            return result;
        }

        public void inOrder(TreeNode? node, ref List<int> result)
        {
            if (node == null)
            {
                return;
            }

            inOrder(node.left, ref result);
            result.Add(node.val);
            inOrder(node.right, ref result);
        }

        public class TreeNode
        {
            public int val { get; set; }
            public TreeNode? left { get; set; }
            public TreeNode? right { get; set; }
        }
    }
}
