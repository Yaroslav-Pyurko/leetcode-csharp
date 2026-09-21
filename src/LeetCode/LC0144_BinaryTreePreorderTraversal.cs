using LeetCode.Common;

namespace LeetCode
{
    public class LC0144_BinaryTreePreorderTraversal
    {
        public IList<int> PreorderTraversal(TreeNode? root)
        {
            var result = new List<int>();
            PreOrder(root, result);
            return result;
        }

        private void PreOrder(TreeNode? node, List<int> result)
        {
            if (node is null)
            {
                return;
            }

            result.Add(node.val);
            PreOrder(node.left, result);
            PreOrder(node.right, result);
        }
    }
}
