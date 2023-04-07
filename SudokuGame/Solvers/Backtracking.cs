using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Solvers
{
    public class Backtracking
    {
        private readonly int[,] _board = null!;

        public Backtracking(int[,] board)
        {
            _board = board;
        }

        // Backtracking
        public bool SolveSudoku()
        {

            // Find the next empty cell
            int row = -1;
            int col = -1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1)
                {
                    break;
                }
            }

            // If there are no more empty cells, we have found a solution
            if (row == -1)
            {
                return true;
            }

            return PossibleNumbers(row, col);
        }

        private bool PossibleNumbers(int row, int col)
        {
            // Try each possible value for the empty cell
            for (int num = 1; num <= 9; num++)
            {
                // Check if the value is valid for the cell
                if (IsValidMove(row, col, num))
                {
                    // Assign the value to the cell
                    _board[row, col] = num;

                    // Recursively try to solve the modified board
                    if (SolveSudoku())
                    {
                        return true;

                    }
                    // If no solution is found, undo the assignment and try the next value
                    _board[row, col] = 0;
                }
            }

            return false;
        }

        private bool IsValidMove(int row, int col, int num)
        {
            // Check if the value is already in the same row or column
            for (int i = 0; i < 9; i++)
            {
                if (_board[row, i] == num || _board[i, col] == num)
                {
                    return false;
                }
            }

            // Check 3x3 sub-grid
            int startRow = row - row % 3;   // Always 0
            int startCol = col - col % 3;

            for (int i = startRow; i < startRow + 3; i++)   // 0, 1, 2
            {
                for (int j = startCol; j < startCol + 3; j++)   //0, 1, 2
                {
                    if (_board[i, j] == num)     // i = {0, 1, 2}; j = {0, 1, 2}
                    {
                        return false;
                    }
                }
            }

            // If the value is not in the same row, column, or box, it is valid
            return true;
        }
    }
}
