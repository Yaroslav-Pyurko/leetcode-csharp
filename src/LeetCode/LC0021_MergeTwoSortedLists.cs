using LeetCode.Common;

namespace LeetCode
{
    public class LC0021_MergeTwoSortedLists
    {
        public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
        {
            var dummy = new ListNode(0);
            var current = dummy;

            while (list1 is not null && list2 is not null)
            {
                ListNode smaller;

                if (list1.val <= list2.val)
                {
                    smaller = list1;
                    list1 = list1.next;
                }
                else
                {
                    smaller = list2;
                    list2 = list2.next;
                }

                current.next = smaller;
                current = smaller;
            }

            current.next = list1 ?? list2;

            return dummy.next;
        }
    }
}
