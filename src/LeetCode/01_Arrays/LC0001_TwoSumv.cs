namespace LeetCode.Arrays
{
    public class LC0001_TwoSum
    {
        public int[] TwoSum(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int diff = target - nums[i];

                if (map.TryGetValue(diff, out var idx))
                    return [idx, i];

                map[nums[i]] = i;
            }

            return [];
        }
    }
}
