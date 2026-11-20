namespace LeetCode.Benchmarks;

/// <summary>
/// Builds the jagged char grids used by the LC0200 benchmarks.
/// </summary>
public static class GridGenerator
{
    /// <summary>
    /// Creates a pseudo-random grid of '1' (land) and '0' (water) cells.
    /// </summary>
    /// <param name="rows">Number of rows.</param>
    /// <param name="cols">Number of columns.</param>
    /// <param name="landDensity">Probability that a given cell is land, in [0, 1].</param>
    /// <param name="seed">
    /// Fixed seed - the same grid must be produced on every run, otherwise the
    /// benchmark compares implementations on different inputs.
    /// </param>
    public static char[][] Create(int rows, int cols, double landDensity, int seed)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cols);

        var random = new Random(seed);
        var grid = new char[rows][];

        for (int row = 0; row < rows; row++)
        {
            var line = new char[cols];

            for (int col = 0; col < cols; col++)
            {
                line[col] = random.NextDouble() < landDensity ? '1' : '0';
            }

            grid[row] = line;
        }

        return grid;
    }

    /// <summary>
    /// Deep-copies a grid. Both benchmarked solutions overwrite land cells with
    /// water while traversing, so each invocation needs its own copy.
    /// </summary>
    public static char[][] Clone(char[][] grid)
    {
        var copy = new char[grid.Length][];

        for (int row = 0; row < grid.Length; row++)
        {
            copy[row] = (char[])grid[row].Clone();
        }

        return copy;
    }
}
