using LeetCode.Common;

namespace LeetCode.Tests
{
    public class LC2236_RootEqualsSumOfChildrenTests
    {
        private readonly LC2236_RootEqualsSumOfChildren _solution = new();

        [Theory]
        [InlineData(new int[] { 10, 4, 6 }, true)]   // Sum matches root exactly 10
        [InlineData(new int[] { 5, 2, 3 }, true)]    // Sum matches root exactly 5
        [InlineData(new int[] { 5, 3, 3 }, false)]   // Sum is 1 more than root
        [InlineData(new int[] { 5, 3, 1 }, false)]   // Sum is 1 less than root
        public void CheckTree_RootEqualsSumOfChildren_ReturnsExpectedResult(int[] treeValues, bool expected)
        {
            int?[] nullableTreeValues = Array.ConvertAll(treeValues, x => (int?)x);
            var root = TreeBuilder.Build(nullableTreeValues);

            var actual = _solution.CheckTree(root);
            
            Assert.Equal(expected, actual);
        }
    }
}