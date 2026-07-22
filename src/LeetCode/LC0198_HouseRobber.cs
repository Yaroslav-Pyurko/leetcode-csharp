namespace LeetCode
{
    public class LC0198_HouseRobber
    {
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            if (nums.Length == 1) return nums[0];
            int prev1 = 0; // Max money that can be robbed up to the previous house
            int prev2 = 0; // Max money that can be robbed up to the house before the previous house
            foreach (var num in nums)
            {
                int temp = prev1;
                prev1 = Math.Max(prev2 + num, prev1); // Choose to rob this house or not
                prev2 = temp; // Update prev2 to the old prev1
            }
            return prev1; // The maximum amount that can be robbed
        }
    }
}