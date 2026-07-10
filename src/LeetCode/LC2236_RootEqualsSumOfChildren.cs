using LeetCode.Common;

namespace LeetCode
{
    public class LC2236_RootEqualsSumOfChildren
    {
        public bool CheckTree(TreeNode root)
        {
            if (root.val == root.left.val + root.right.val)
            {
                return true;
            }
            return false;
        }
    }
}