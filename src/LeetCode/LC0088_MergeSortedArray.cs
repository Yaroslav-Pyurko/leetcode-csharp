namespace LeetCode;

public class LC0088_MergeSortedArray
{
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // Required data validation
        if (n == 0)
        {
            return;
        }
        if (m == 0)
        {
            Array.Copy(nums2, 0, nums1, 0, n);
            return;
        }

        // Probable and fastest path 1, when merging data chunks
        if (nums1[m - 1] <= nums2[0])
        {
            Array.Copy(nums2, 0, nums1, m, n);
            return;
        }
        if (nums2[n - 1] <= nums1[0])
        {
            Array.Copy(nums1, 0, nums1, n, m);
            Array.Copy(nums2, 0, nums1, 0, n);
            return;
        }

        // General case, merge from the back
        var i = m - 1;
        var j = n - 1;
        var k = m + n - 1;

        while (i >= 0 && j >= 0)
        {
            if (nums1[i] > nums2[j])
            {
                nums1[k] = nums1[i];
                i--;
            }
            else
            {
                nums1[k] = nums2[j];
                j--;
            }
            k--;
        }

        if (j >= 0)
        {
            Array.Copy(nums2, 0, nums1, 0, j + 1);
        }
    }
}
