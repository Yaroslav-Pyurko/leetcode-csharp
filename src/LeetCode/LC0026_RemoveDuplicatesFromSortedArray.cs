namespace LeetCode
{
    public class LC0026_RemoveDuplicatesFromSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            var uniqueNums = new SortedSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                uniqueNums.Add(nums[i]);
            }

            // set array
            //nums = hashSet.ToArray(); // doesn't work
            for (int i = 0; i < uniqueNums.Count; i++)
            {
                nums[i] = uniqueNums.ElementAt(i);
            }

            return uniqueNums.Count();
        }
    }
}