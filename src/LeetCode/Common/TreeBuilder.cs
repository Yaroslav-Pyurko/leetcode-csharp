namespace LeetCode.Common
{
    public static class TreeBuilder
    {
        public static TreeNode? Build(params int?[] values)
        {
            if (values.Length == 0 || values[0] == null)
                return null;

            var root = new TreeNode(values[0]!.Value);
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int i = 1;

            while (queue.Count > 0 && i < values.Length)
            {
                var node = queue.Dequeue();

                if (i < values.Length && values[i] != null)
                {
                    node.left = new TreeNode(values[i]!.Value);
                    queue.Enqueue(node.left);
                }
                i++;

                if (i < values.Length && values[i] != null)
                {
                    node.right = new TreeNode(values[i]!.Value);
                    queue.Enqueue(node.right);
                }
                i++;
            }

            return root;
        }
    }
}