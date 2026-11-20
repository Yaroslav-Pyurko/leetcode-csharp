namespace LeetCode.Tests;

public class LC0509_FibonacciNumberTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 3)]
    [InlineData(10, 55)]
    [InlineData(30, 832_040)]        // upper bound of the LeetCode constraints
    [InlineData(46, 1_836_311_903)]  // largest Fibonacci number that fits in int
    public void Fib_ReturnsExpectedResult(int n, int expected)
    {
        var solution = new LC0509_FibonacciNumber();

        var actual = solution.Fib(n);

        Assert.Equal(expected, actual);
    }
}
