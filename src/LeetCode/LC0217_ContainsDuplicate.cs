using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public class LC0217_ContainsDuplicate
    {
        public bool ContainsDuplicate(int[] nums)
        {
            var nums2 = new List<int>();
            foreach (var n in nums)
            {
                if (nums2.Contains(n))
                {
                    return true;
                }
                nums2.Add(n);
            }
            return false;
        }
    }
}
