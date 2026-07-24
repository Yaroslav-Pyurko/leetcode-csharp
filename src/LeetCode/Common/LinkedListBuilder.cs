namespace LeetCode.Common
{
    public static class LinkedListBuilder
    {
        // Converts an array into a linked list of ListNode
        public static ListNode? ToListNode(int[] values)
        {
            ListNode? head = null;
            ListNode? tail = null;

            foreach (var value in values)
            {
                var node = new ListNode(value);

                if (head is null)
                {
                    head = node;
                }
                else
                {
                    tail!.next = node;
                }

                tail = node;
            }

            return head;
        }

        // Converts a linked list back into an array for comparison
        public static int[] ToArray(ListNode? head)
        {
            var result = new List<int>();

            while (head is not null)
            {
                result.Add(head.val);
                head = head.next;
            }

            return result.ToArray();
        }
    }
}
