using LeetCode.Common;

namespace LeetCode
{
    public class LC2236_RootEqualsSumOfChildren
    {
        public bool CheckTree(TreeNode root)
        {
            return root.val == root.left!.val + root.right!.val;
        }
    }
}
