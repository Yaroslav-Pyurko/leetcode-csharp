namespace LeetCode
{
    public class LC0007_ReverseInteger
    {
        public int Reverse(int x)
        {
            var result = 0;

            while (x != 0)
            {
                var lastDigit = x % 10;
                var temp = result * 10 + lastDigit; // Add last digit to Result

                // In case of overflow, the current value will not be equal to the previous one
                var preResult = (temp - lastDigit) / 10;
                if (preResult != result)
                {
                    return 0;
                }

                result = temp;
                x /= 10; // next digit
            }
            return result;
        }
    }
}