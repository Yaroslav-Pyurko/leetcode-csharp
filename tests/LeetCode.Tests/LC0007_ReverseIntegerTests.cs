namespace LeetCode.Tests
{
    public class LC0007_ReverseIntegerTests
    {
        [Theory]
        [InlineData(123, 321)]
        [InlineData(-123, -321)]
        [InlineData(120, 21)]
        [InlineData(2_147_483_647, 0)] // 2^32-1, overflow case
        [InlineData(111_222_333, 333_222_111)]
        public void Reverse_ReturnsExpectedResult(int input, int expected)
        {
            var solution = new LC0007_ReverseInteger();
            var actual = solution.Reverse(input);
            Assert.Equal(expected, actual);
        }
    }
}