namespace LeetCode;

public class LC0200_NumberOfIslands
{
    public int NumIslands(char[][] grid)
    {
        int cols = grid[0].Length;
        int rows = grid.Length;
        int numIslands = 0;

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (grid[row][col] == '1')
                {
                    numIslands++;
                    Dfs(grid, col, row, cols, rows);
                }
            }
        }
        return numIslands;
    }

    /// <summary>
    /// Depth-first search to mark all connected land cells ('1') as visited ('0').
    /// </summary>
    private void Dfs(char[][] grid, int col, int row, int cols, int rows)
    {
        if (col < 0     ||
            col >= cols ||
            row < 0     ||
            row >= rows ||
            grid[row][col] == '0')
        {
            return;
        }

        // Set the current cell to mark it as visited
        grid[row][col] = '0';
        Dfs(grid, col + 1, row, cols, rows);
        Dfs(grid, col - 1, row, cols, rows);
        Dfs(grid, col, row + 1, cols, rows);
        Dfs(grid, col, row - 1, cols, rows);
    }
}
