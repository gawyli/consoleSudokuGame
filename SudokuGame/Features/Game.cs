using SudokuGame.Models;
using SudokuGame.Solvers;
using SudokuGame.Utilities;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace SudokuGame.Features
{
    public class Game
    {
        private static PlayerData Player { get; set; } = null!;

        private static Random _random = new Random();

        private static int[,] _tempBoard = null!;

        public static bool NewGame(PlayerData player)
        {
            Player = player;

            _tempBoard = new int[Player.BoardSize, Player.BoardSize];

            GenerateBoard();
            GenerateLevel(Player.Level);

            return true;
        }

        private static void GenerateBoard()
        {
            /* If we create three rows - only for 9x9
            bool ready = GenerateThreeRows();
            while (!ready)
            {
                ready = GenerateThreeRows();
            }
            */

            GenerateFirstRow();

            var backtracking = new Backtracking(_tempBoard);
            backtracking.SolveSudoku();

            Player!.Board = (int[,])_tempBoard.Clone();
        }

        // A method that generates first row with random numbers
        private static void GenerateFirstRow()
        {
            List<int> row = new List<int>();
            row.AddRange(Enumerable.Range(1, Player.BoardSize).OrderBy(n => _random.Next()));

            for (int j = 0; j < Player.BoardSize; j++)
            {

                _tempBoard[0, j] = row[j];

            }
        }

        // A method that generates first, fourth and seventh row with random numbers
        private static bool GenerateThreeRows()
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

        // A method that picks the amount number to be remove form the the board accordingly to the chosen level by the player
        private static void GenerateLevel(int selectedLevel)
        {
            if (Player.BoardSize == 4)
            {
                switch (selectedLevel)
                {
                    case 0:
                        //HideNumbers(_random.Next(30, 35));    // Extremally Easy
                        HideNumbers(1);
                        break;
                    case 1:
                        HideNumbers(_random.Next(2, 3));    // Easy
                        break;
                    case 2:
                        HideNumbers(_random.Next(3, 4));    // Medium
                        break;
                    case 3:
                        HideNumbers(_random.Next(4, 5));    // Difficult
                        break;
                    case 4:
                        HideNumbers(_random.Next(5, 6));    //Evil
                        break;
                }
            }
            else if (Player.BoardSize == 9)
            {
                switch (selectedLevel)
                {
                    case 0:
                        HideNumbers(_random.Next(30, 35));    // Extremally Easy  
                        break;
                    case 1:
                        HideNumbers(_random.Next(35, 45));    // Easy
                        break;
                    case 2:
                        HideNumbers(_random.Next(45, 49));    // Medium
                        break;
                    case 3:
                        HideNumbers(_random.Next(49, 53));    // Difficult
                        break;
                    case 4:
                        HideNumbers(_random.Next(53, 58));    // Extremally Difficult -> min 24 clues (58)
                        break;
                }
            }
            else
            {
                switch (selectedLevel)
                {
                    case 0:
                        HideNumbers(_random.Next(30, 50));    // Extremally Easy
                        break;
                    case 1:
                        HideNumbers(_random.Next(50, 100));    // Easy
                        break;
                    case 2:
                        HideNumbers(_random.Next(100, 130));    // Medium
                        break;
                    case 3:
                        HideNumbers(_random.Next(130, 150));    // Difficult
                        break;
                    case 4:
                        HideNumbers(_random.Next(150, 156));    // Extremally Difficult -> min 101 clues (155)
                        break;
                }
            }   
        }

        // A method that removes a given number of cells from a Sudoku board while ensuring that the board has a unique solution
        private static void HideNumbers(int numbersToRemove)
        {
            int attempts = 0;

            for (int i = 1; i <= numbersToRemove; i++)
            {
                //Console.WriteLine(i);

                int row = _random.Next(0, Player.BoardSize);    // Y
                int col = _random.Next(0, Player.BoardSize);    // X

                string hideNumbKey = row.ToString() + "," + col.ToString();   // Cords "y,x" as a key
                int hideNumbTemp;

                while (Player!.HideNumbers.ContainsKey(hideNumbKey))
                {
                    row = _random.Next(0, Player.BoardSize);
                    col = _random.Next(0, Player.BoardSize);

                    hideNumbKey = row.ToString() + "," + col.ToString();
                }

                hideNumbTemp = _tempBoard![row, col];
                _tempBoard[row, col] = 0;


                if (!ConstraintProg.HasMultipleSolutions(_tempBoard))
                {
                    // If not, restore the original value of the cell
                    _tempBoard[row, col] = hideNumbTemp;
                    i--;
                    attempts++;

                    // Restore penultimate value to the original value if last original value don't bring back the board to the unique state 
                    if (attempts > 1)
                    {
                        string lastCords = Player!.HideNumbers.Keys.Last();

                        CordsUtil.GetCords(lastCords, out row, out col);

                        Player!.Board[row, col] = Player!.HideNumbers[lastCords];
                        Player!.HideNumbers.Remove(lastCords);

                        _tempBoard = (int[,])Player!.Board.Clone();

                        attempts = 0;
                        i--;
                    }
                }
                else
                {
                    Player!.Board[row, col] = 0;
                    //_tempBoard = (int[,])Player!.Board.Clone();

                    Player.HideNumbers.Add(hideNumbKey, hideNumbTemp);

                    attempts--;
                }
            }
        }
    }
}
