namespace LeetCode
{
    public class LC0026_RemoveDuplicatesFromSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            int k = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] != nums[k - 1])
                {
                    nums[k++] = nums[i];
                }
            }
            return k;
        }
    }
}