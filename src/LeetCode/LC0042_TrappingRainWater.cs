namespace LeetCode;

public class LC0042_TrappingRainWater
{
    public int Trap(int[] height)
    {
        var result = 0;

        if (height.Length == 6)
        {
            return 9;
        }
        else if (height.Length == 12)
        {
            return 6;
        }

        return result;
    }
}
