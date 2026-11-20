namespace LeetCode.Benchmarks.Baselines;

/// <summary>
/// Reference implementation used only as a benchmark counterpart to the
/// recursive solution in <c>src/LeetCode/LC0200_NumberOfIslands.cs</c>.
///
/// Same O(rows * cols) work, but the traversal state lives on an explicit
/// stack instead of the call stack, so it cannot blow the 1 MB thread stack
/// on a grid that is one big island. It also walks the grid row-major,
/// which matches how a jagged array is laid out in memory.
///
/// This is deliberately not in src/ - it is not a submitted LeetCode answer.
/// </summary>
public sealed class LC0200_IterativeDfs
{
    public int NumIslands(char[][] grid)
    {
        if (grid is null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int rows = grid.Length;
        int cols = grid[0].Length;
        int numIslands = 0;
        var stack = new Stack<(int Row, int Col)>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row][col] != '1')
                {
                    continue;
                }

                numIslands++;
                Visit(grid, stack, row, col, rows, cols);

                while (stack.Count > 0)
                {
                    var (r, c) = stack.Pop();

                    Visit(grid, stack, r - 1, c, rows, cols);
                    Visit(grid, stack, r + 1, c, rows, cols);
                    Visit(grid, stack, r, c - 1, rows, cols);
                    Visit(grid, stack, r, c + 1, rows, cols);
                }
            }
        }

        return numIslands;
    }

    /// <summary>
    /// Marks a land cell as visited and queues it, ignoring out-of-range and water cells.
    /// Marking on push (rather than on pop) keeps a cell from entering the stack twice.
    /// </summary>
    private static void Visit(char[][] grid, Stack<(int Row, int Col)> stack, int row, int col, int rows, int cols)
    {
        // Single unsigned compare covers both the negative and the too-large case.
        if ((uint)row >= (uint)rows || (uint)col >= (uint)cols || grid[row][col] != '1')
        {
            return;
        }

        grid[row][col] = '0';
        stack.Push((row, col));
    }
}
