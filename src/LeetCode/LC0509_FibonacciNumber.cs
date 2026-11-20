namespace LeetCode;

public class LC0509_FibonacciNumber
{
    public int Fib(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        int previous = 0; // F(i - 2)
        int current = 1;  // F(i - 1)

        for (int i = 2; i <= n; i++)
        {
            int next = previous + current;
            previous = current;
            current = next;
        }

        return current;
    }
}
