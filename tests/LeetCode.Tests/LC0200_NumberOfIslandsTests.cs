namespace LeetCode.Tests
{
    public class LC0200_NumberOfIslandsTests
    {
        [Theory]
        [InlineData(new[] {
            "11110",
            "11010",
            "11000",
            "00000"
            }, 1)]
        [InlineData(new[] {
            "11000",
            "11000",
            "00100",
            "00011"
            }, 3)]
        [InlineData(new[] { "1" }, 1)]
        [InlineData(new[] { "0" }, 0)]
        [InlineData(new[] { "00", "00" }, 0)]
        public void NumIslands_ReturnsExpectedCount(string[] rows, int expected)
        {
            var grid = rows.Select(row => row.ToCharArray()).ToArray();
            var solution = new LC0200_NumberOfIslands();

            var actual = solution.NumIslands(grid);

            Assert.Equal(expected, actual);
        }
    }
}
