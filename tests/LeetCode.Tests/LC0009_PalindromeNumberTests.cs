namespace LeetCode.Tests
{
    public class LC0009_PalindromeNumberTests
    {
        [Fact]
        public void Case1()
        {
            var solution = new LC0009_PalindromeNumber();
            var actual = solution.IsPalindrome(121);
            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case2()
        {
            var solution = new LC0009_PalindromeNumber();
            var actual = solution.IsPalindrome(-121);
            var expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case3()
        {
            var solution = new LC0009_PalindromeNumber();
            var actual = solution.IsPalindrome(10);
            var expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Case4()
        {
            var solution = new LC0009_PalindromeNumber();
            var actual = solution.IsPalindrome(7);
            var expected = true;
            Assert.Equal(expected, actual);
        }
    }
}