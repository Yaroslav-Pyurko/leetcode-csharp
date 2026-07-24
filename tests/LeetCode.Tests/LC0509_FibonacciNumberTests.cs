namespace LeetCode.Tests
{
    public class LC0509_FibonacciNumberTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0509_FibonacciNumber();

            var actual = solution.Fib(30);

            Assert.Equal(832_040, actual);
        }
    }
}