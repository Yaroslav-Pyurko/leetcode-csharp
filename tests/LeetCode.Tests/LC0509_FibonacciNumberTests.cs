namespace LeetCode.Arrays.Tests
{
    public class LC0509_FibonacciNumberTests
    {
        [Fact]
        public void Case1()
        {
            var s = new LC0509_FibonacciNumber();
            var res = s.Fib(30);
            Assert.Equal(832_040, res);
        }
    }
}