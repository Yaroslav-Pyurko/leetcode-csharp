namespace LeetCode;

public class LC0007_ReverseInteger
{
    private const int MaxBeforeAppendingDigit = int.MaxValue / 10;
    private const int LastDigitOfMaxValue = int.MaxValue % 10;

    public int Reverse(int x)
    {
        if (x == int.MinValue)
        {
            return 0;
        }

        int sign = x < 0 ? -1 : 1;
        x = x < 0 ? -x : x;

        int reversed = 0;

        unchecked
        {
            while (x > 0)
            {
                int pop = x % 10;
                x /= 10;

                if (reversed > MaxBeforeAppendingDigit ||
                    (reversed == MaxBeforeAppendingDigit && pop > LastDigitOfMaxValue))
                {
                    return 0;
                }

                reversed = (reversed * 10) + pop;
            }
        }

        return reversed * sign;
    }
}
