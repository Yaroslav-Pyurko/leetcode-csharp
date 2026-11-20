namespace LeetCode;

public class LC0217_ContainsDuplicate
{
    public bool ContainsDuplicate(int[] nums)
    {
        var seen = new HashSet<int>(nums.Length);
        foreach (var n in nums)
        {
            if (!seen.Add(n))
            {
                return true;
            }
        }
        return false;
    }
}
