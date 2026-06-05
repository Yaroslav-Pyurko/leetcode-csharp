namespace LeetCode._01_Arrays
{
    public class LC0509_FibonacciNumber
    {
        public int Fib(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            return Fib(n - 1) + Fib(n - 2);
        }
    }
}