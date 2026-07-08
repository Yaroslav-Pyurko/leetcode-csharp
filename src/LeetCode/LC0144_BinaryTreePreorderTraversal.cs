using LeetCode.Common;

namespace LeetCode
{
    public class LC0144_BinaryTreePreorderTraversal
    {
        public IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            preOrder(root, ref result);
            return result;
        }

        private void preOrder(TreeNode node, ref List<int> result)
        {
            if (node == null)
            {
                return;
            }

            result.Add(node.val);
            preOrder(node.left, ref result);
            preOrder(node.right, ref result);
        }
    }
}