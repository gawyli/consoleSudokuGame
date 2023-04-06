using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class Game
    {
        private static PlayerData Player { get; set; } = null!;

        private static Random _random = new Random();

        private static int[,] _loadedGame = null!;
        private static int[,] _tempBoard = null!;
        private static int[,] _secondSolutionBoard = null!;
        private static int[,] _firstSolutionBoard = null!;

        public static void NewGame(PlayerData player)
        {
            Player = player;

            _tempBoard = new int[9, 9];
            _firstSolutionBoard = new int[9, 9];
            _secondSolutionBoard = new int[9, 9];

            GenerateBoard();
            GenerateLevel(Player.Level);
        }

        public static void GenerateBoard()
        {
            if (_loadedGame != null)
            {
                Player.Board = _loadedGame;
            }

            bool ready = GenerateThreeRandomRows();
            while (!ready)
            {
                ready = GenerateThreeRandomRows();
            }

            SolveSudoku(false);
            Player!.Board = (int[,])_tempBoard.Clone();   
        }

        private static bool GenerateThreeRandomRows()
        {
            List<int> row = new List<int>();
            List<List<int>> rows = new List<List<int>>();

            row.AddRange(Enumerable.Range(1, 9).OrderBy(n => _random.Next()));
            rows.Add(row);


            for (int i = 0; i < 2; i++)
            {
                row = new List<int>();

                for (int j = 0; j < 9; j++)
                {
                    int possibleNumber = _random.Next(1, 10);
                    while (row.Contains(possibleNumber))
                    {
                        possibleNumber = _random.Next(1, 10);
                    }

                    if (rows[i][j] != possibleNumber)
                    {
                        if (rows.Count > 1)
                        {
                            if (rows[i - 1][j] != possibleNumber)
                            {
                                row.Add(possibleNumber);
                            }
                            else if (j == 8 || j == 7 && rows[i - 1][j] == possibleNumber)
                            {
                                return false;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else
                        {
                            row.Add(possibleNumber);
                        }
                    }
                    else if (j == 8 || j == 7 && rows[i][j] == possibleNumber)
                    {
                        return false;
                    }
                    else
                    {
                        j--;
                    }
                }
                rows.Add(row);
            }

            int nRow = 0;

            for (int i = 0; i < 7; i += 3)
            {

                for (int j = 0; j < 9; j++)
                {

                    _tempBoard[i, j] = rows[nRow][j];

                }
                nRow++;
            }

            return true;
        }

        // Backtracking
        public static bool SolveSudoku(bool secondSolution)
        {
            // Find the next empty cell
            int row = -1;
            int col = -1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_tempBoard[i, j] == 0 || _secondSolutionBoard[i,j] == 0 && secondSolution)
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
            
            return PossibleNumbers(row, col, secondSolution);
        }

        private static bool PossibleNumbers(int row, int col, bool secondSolution)
        {
            if (!secondSolution)
            {
                // Try each possible value for the empty cell
                for (int num = 1; num <= 9; num++)
                {
                    // Check if the value is valid for the cell
                    if (IsValidMove(row, col, num, secondSolution))
                    {
                        // Assign the value to the cell
                        _tempBoard[row, col] = num;

                        // Recursively try to solve the modified board
                        if (SolveSudoku(false))
                        {
                            return true;

                        }
                        // If no solution is found, undo the assignment and try the next value
                        _tempBoard[row, col] = 0;
                    }
                }
            }

            if (secondSolution)
            {
                // Try each possible value for the empty cell
                for (int num = 9; num >= 1; num--)
                {
                    // Check if the value is valid for the cell
                    if (IsValidMove(row, col, num, secondSolution))
                    {
                        // Assign the value to the cell
                        _secondSolutionBoard[row, col] = num;

                        // Recursively try to solve the modified board
                        if (SolveSudoku(true))
                        {
                            return true;

                        }
                        // If no solution is found, undo the assignment and try the next value
                        _secondSolutionBoard[row, col] = 0;
                    }
                }
            }

            return false;
        }

        private static bool IsValidMove(int row, int col, int num, bool secondSolution)
        {
            if (!secondSolution)
            {
                // Check if the value is already in the same row or column
                for (int i = 0; i < 9; i++)
                {
                    if (_tempBoard[row, i] == num || _tempBoard[i, col] == num)
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
                        if (_tempBoard[i, j] == num)     // i = {0, 1, 2}; j = {0, 1, 2}
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                // Check if the value is already in the same row or column
                for (int i = 0; i < 9; i++)
                {
                    if (_secondSolutionBoard[row, i] == num || _secondSolutionBoard[i, col] == num)
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
                        if (_secondSolutionBoard[i, j] == num)     // i = {0, 1, 2}; j = {0, 1, 2}
                        {
                            return false;
                        }
                    }
                }
            }
            
            // If the value is not in the same row, column, or box, it is valid
            return true;
        }

        // A method that picks the amount number to be remove form the the board accordingly to the chosen level by the player
        private static void GenerateLevel(int selectedLevel)
        {
            switch (selectedLevel)
            {
                case 0:
                    //HideNumbers(_random.Next(30, 35));    // Extremally Easy
                    HideNumbers(1);
                    break;
                case 1:
                    HideNumbers(_random.Next(35, 45));    // Easy
                    break;
                case 2:
                    HideNumbers(_random.Next(46, 49));    // Medium
                    break;
                case 3:
                    HideNumbers(_random.Next(50, 53));    // Difficult
                    break;
                case 4: 
                    HideNumbers(_random.Next(54, 56));    // Extremally Difficult
                    break;
            }
        }

        // A method that removes a given number of cells from a Sudoku board while ensuring that the board has a unique solution
        private static void HideNumbers(int numbersToRemove)
        {
            _firstSolutionBoard = (int[,])_tempBoard.Clone();
            int attempts = 0;

            for (int i = 1; i <= numbersToRemove; i++)
            {
                
                int row = _random.Next(0, 9);    // Y
                int col = _random.Next(0, 9);    // X

                string hiddenNumbKey = row.ToString() + "," + col.ToString();   // Cords "y , x" as a key
                int hiddenNumbTemp;

                while (Player!.HideNumbers.ContainsKey(hiddenNumbKey))
                {
                    row = _random.Next(0, 9);
                    col = _random.Next(0, 9);

                    hiddenNumbKey = row.ToString() + "," + col.ToString();
                }

                hiddenNumbTemp = _tempBoard![row, col];
                _tempBoard[row, col] = 0;


                if (!HasUniqueSolution())
                {
                    // If not, restore the original value of the cell
                    _tempBoard[row, col] = hiddenNumbTemp;
                    i--;
                    attempts++;

                    // Restore second last to the original value if last original value do not back the board to the unique state 
                    if (attempts > 1)
                    {
                        string lastCords = Player!.HideNumbers.Keys.Last();

                        string[] cords = lastCords.Split(',');
                        row = int.Parse(cords[0]);
                        col = int.Parse(cords[1]);

                        hiddenNumbTemp = Player!.HideNumbers[lastCords];

                        Player!.HideNumbers.Remove(lastCords);

                        Player!.Board[row, col] = hiddenNumbTemp;
                        _tempBoard = (int[,])Player!.Board.Clone();

                        attempts = 0;
                        i--;
                    }
                }
                else
                {
                    Player!.Board[row, col] = 0;
                    _tempBoard = (int[,])Player!.Board.Clone();
                    Player.HideNumbers.Add(hiddenNumbKey, hiddenNumbTemp);
                }
            }
        }

        // A helper method that checks if a given Sudoku board has a unique solution
        private static bool HasUniqueSolution()
        {
            _secondSolutionBoard = (int[,])_tempBoard.Clone();

            SolveSudoku(false);
            SolveSudoku(true);

            var equal = _tempBoard.Rank == _secondSolutionBoard!.Rank &&
                        Enumerable.Range(0, _tempBoard.Rank).All(dimension => _tempBoard.GetLength(dimension) == _secondSolutionBoard.GetLength(dimension)) &&
                        _tempBoard.Cast<int>().SequenceEqual(_secondSolutionBoard.Cast<int>());

            bool unique = false;

            if (equal)
            {
                unique = _tempBoard.Rank == _firstSolutionBoard!.Rank &&
                         Enumerable.Range(0, _tempBoard.Rank).All(dimension => _tempBoard.GetLength(dimension) == _firstSolutionBoard.GetLength(dimension)) &&
                         _tempBoard.Cast<int>().SequenceEqual(_firstSolutionBoard.Cast<int>());
            }

            return unique;
        }
    }
}
