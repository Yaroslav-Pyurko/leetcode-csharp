namespace LeetCode
{
    public class LC0007_ReverseInteger
    {
        public int Reverse(int x)
        {
            if (x == int.MinValue) return 0;

            int sign = x < 0 ? -1 : 1;
            x = x < 0 ? -x : x;

            int reversed = 0;

            unchecked
            {
                while (x > 0)
                {
                    int pop = x % 10;
                    x /= 10;

                    // int.MaxValue = 2_147_483_647 : 10 = 214_748_364 
                    if (reversed > 214_748_364 || (reversed == 214_748_364 && pop > 7))
                    {
                        return 0;
                    }

                    reversed = (reversed * 10) + pop;
                }
            }

            return reversed * sign;
        }
    }
}