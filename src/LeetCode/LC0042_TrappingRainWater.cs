namespace LeetCode;

public class LC0042_TrappingRainWater
{
    public int Trap(int[] height)
    {
        var left = 0;
        var right = height.Length - 1;
        var leftMax = 0;
        var rightMax = 0;
        var water = 0;

        while (left < right)
        {
            leftMax = Math.Max(leftMax, height[left]);
            rightMax = Math.Max(rightMax, height[right]);

            if (leftMax < rightMax)
            {
                water += leftMax - height[left];
                left++;
            }
            else
            {
                water += rightMax - height[right];
                right--;
            }
        }

        return water;
    }
}
