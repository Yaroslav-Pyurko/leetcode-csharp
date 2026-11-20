namespace LeetCode.Common;

public static class LinkedListBuilder
{
    /// <summary>
    /// Converts an array into a linked list of ListNode
    /// </summary>
    /// <param name="values">The array of integers to convert</param>
    /// <returns>The head of the linked list</returns>
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

    /// <summary>
    /// Converts a linked list back into an array for comparison
    /// </summary>
    /// <param name="head">The head of the linked list</param>
    /// <returns>An array containing the values of the linked list</returns>
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
