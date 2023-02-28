using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class Game
    {
        private static UserConfig? UserCfg { get; set; }
        private static int[,] _board = null!;
        private static Random _random = new Random();
        private static int[,] _loadedGame = null!;

        public Game()
        {
            
        }

        //public Game(int[,] loadedGame)
        //{
        //    _loadedGame = loadedGame;
        //}

        public static void GenerateBoard(UserConfig userConfig)
        {
            UserCfg = userConfig;

            _board = new int[9, 9];

            if (_loadedGame != null)
            {
                _board = _loadedGame;
            }

            bool ready = GenerateThreeRandomRows();
            while (!ready)
            {
                ready = GenerateThreeRandomRows();
            }

            SolveSudoku(0, 0);
            ChosenLevel(UserCfg.Level);

            UserCfg.Board = _board;
        }

        public static void ResetBoard()
        {
            foreach (var key in UserCfg!.HidingNumbers.Keys) 
            {
                string[] cords = key.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                _board[row, col] = 0;
            }
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

                    _board[i, j] = rows[nRow][j];

                }
                nRow++;
            }

            return true;
        }

        // Backtracking
        private static bool SolveSudoku(int row, int col)
        {
            if (row == 9)
            {
                return true;
            }

            if (_board?[row, col] != 0)
            {
                if (col == 8)
                {
                    if (SolveSudoku(row + 1, 0))
                        return true;
                }
                else
                {
                    if (SolveSudoku(row, col + 1))
                        return true;
                }

                return false;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (IsValidMove(row, col, i))
                {
                    _board[row, col] = i;

                    if (col == 8)
                    {
                        if (SolveSudoku(row + 1, 0))
                            return true;
                    }
                    else
                    {
                        if (SolveSudoku(row, col + 1))
                            return true;
                    }

                    _board[row, col] = 0;
                }
            }

            return false;
        }

        private static bool IsValidMove(int row, int col, int num)
        {
            // Check row
            for (int i = 0; i < 9; i++)
            {
                if (_board?[row, i] == num)
                    return false;
            }

            // Check column
            for (int i = 0; i < 9; i++)
            {
                if (_board?[i, col] == num)
                    return false;
            }

            // Check 3x3 sub-grid
            int startRow = row - row % 3;   // Always 0
            int startCol = col - col % 3;

            for (int i = startRow; i < startRow + 3; i++)   // 0, 1, 2
            {
                for (int j = startCol; j < startCol + 3; j++)   //0, 1, 2
                {
                    if (_board?[i, j] == num)     // i = {0, 1, 2}; j = {0, 1, 2}
                        return false;
                }
            }

            return true;
        }

        // Generating empty cells
        private static void ChosenLevel(int level)
        {
            int easy = 0, medium = 0, hard = 0;

            switch (level)
            {
                case 0:
                    easy = _random.Next(39, 41);
                    HideNumbers(easy);
                    break;
                case 1:
                    medium = _random.Next(41, 50);
                    HideNumbers(medium);
                    break;
                case 2:
                    hard = _random.Next(51, 60);
                    HideNumbers(hard);
                    break;
            }
        }

        private static void HideNumbers(int chosenLevel)
        {
            UserCfg!.HidingNumbers = new Dictionary<string, int>();

            for (int i = 0; i <= chosenLevel; i++)
            {
                int row = _random.Next(0, 9);    // Y
                int col = _random.Next(0, 9);    // X

                string hiddenNumbKey = row.ToString() + "," + col.ToString();   // Cords "y , x" as a key
                int hiddenNumb;

                while (UserCfg.HidingNumbers.ContainsKey(hiddenNumbKey))
                {
                    row = _random.Next(0, 9);
                    col = _random.Next(0, 9);

                    hiddenNumbKey = row.ToString() + "," + col.ToString();
                }

                hiddenNumb = _board[row, col];
                UserCfg.HidingNumbers.Add(hiddenNumbKey, hiddenNumb);

                _board[row, col] = 0;

            }
        } 
    }
}
