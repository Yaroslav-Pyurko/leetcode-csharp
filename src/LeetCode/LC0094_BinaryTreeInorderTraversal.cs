using LeetCode.Common;

namespace LeetCode
{
    public class LC0094_BinaryTreeInorderTraversal
    {
        public IList<int> InorderTraversal(TreeNode? root)
        {
            var result = new List<int>();
            InOrder(root, result);
            return result;
        }

        private void InOrder(TreeNode? node, List<int> result)
        {
            if (node is null)
            {
                return;
            }

            InOrder(node.left, result);
            result.Add(node.val);
            InOrder(node.right, result);
        }
    }
}
