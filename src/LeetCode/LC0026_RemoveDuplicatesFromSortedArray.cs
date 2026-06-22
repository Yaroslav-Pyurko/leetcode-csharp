namespace LeetCode
{
    public class LC0026_RemoveDuplicatesFromSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            var j = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] != nums[i - 1])
                {
                    nums[j] = nums[i];
                    j++;
                }
            }
            return j;
        }
    }
}