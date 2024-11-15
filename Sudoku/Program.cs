namespace Sudoku
{
    internal class Program
    {
        public static bool ValidateSudoku(int[][] grid)
        {
            //Validation for size
            int n = grid.Length;

            if (n <= 0)
                return false;

            if (grid.Any(row => row.Length != n))
                return false;

            if (Math.Sqrt(n) % 1 != 0)
                return false;

            int sqrtN = (int)Math.Sqrt(n);
            int[] requiredNumbers = Enumerable.Range(1, n).ToArray();

            //Validation for rows and columns
            for (int i = 0; i < n; i++)
            {
                if (!ContainsAllNumbers(grid[i], requiredNumbers) ||
                    !ContainsAllNumbers(grid.Select(row => row[i]).ToArray(), requiredNumbers))
                    return false;
            }

            //Validation for little squares
            for (int rowStart = 0; rowStart < n; rowStart += sqrtN)
            {
                for (int colStart = 0; colStart < n; colStart += sqrtN)
                {
                    if (!ContainsAllNumbers(GetSubgrid(grid, rowStart, colStart, sqrtN), requiredNumbers))
                        return false;
                }
            }

            return true;
        }

        private static bool ContainsAllNumbers(int[] values, int[] requiredNumbers)
        {
            return requiredNumbers.All(values.Contains);
        }

        private static int[] GetSubgrid(int[][] grid, int rowStart, int colStart, int size)
        { 
            return Enumerable.Range(rowStart, size)
                .SelectMany(row => Enumerable.Range(colStart, size)
                .Select(col => grid[row][col]))
                .ToArray();
        }
        static void Main(string[] args)
        {
            int[][] goodSudoku = {
            new int[] {7,8,4, 1,5,9, 3,2,6},
            new int[] {5,3,9, 6,7,2, 8,4,1},
            new int[] {6,1,2, 4,3,8, 7,5,9},
            new int[] {9,2,8, 7,1,5, 4,6,3},
            new int[] {3,5,7, 8,4,6, 1,9,2},
            new int[] {4,6,1, 9,2,3, 5,8,7},
            new int[] {8,7,6, 3,9,4, 2,1,5},
            new int[] {2,4,3, 5,6,1, 9,7,8},
            new int[] {1,9,5, 2,8,7, 6,3,4}
        };
            Console.WriteLine(ValidateSudoku(goodSudoku));

            int[][] badSudoku = {
            new int[] {7,8,4, 1,5,9, 3,2,6},
            new int[] {5,3,9, 6,7,2, 8,4,1},
            new int[] {6,1,2, 4,3,8, 7,5,9},
            new int[] {9,2,8, 7,1,5, 4,6,3},
            new int[] {3,5,7, 8,4,6, 1,9,2},
            new int[] {4,6,1, 9,2,3, 5,8,7},
            new int[] {8,7,6, 3,9,4, 2,1,5},
            new int[] {2,4,3, 5,6,1, 9,7,7}, 
            new int[] {1,9,5, 2,8,7, 6,3,4}
        };

            Console.WriteLine(ValidateSudoku(badSudoku));

        }
    }
}
