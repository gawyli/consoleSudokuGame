using SudokuGame.Models;
using SudokuGame.New;
using SudokuGame.Solvers;
using SudokuGame.Utilities;
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

        private static int[,] _tempBoard = null!;

        public static void NewGame(PlayerData player)
        {
            Player = player;

            _tempBoard = new int[9, 9];

            GenerateBoard();
            GenerateLevel(Player.Level);
        }

        private static void GenerateBoard()
        {
            bool ready = GenerateThreeRows();
            while (!ready)
            {
                ready = GenerateThreeRows();
            }

            var backtracking = new Backtracking(_tempBoard);
            backtracking.SolveSudoku();

            Player!.Board = (int[,])_tempBoard.Clone();   
        }

        // A method that generate first, fourth and seventh row with random numbers
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
                    HideNumbers(_random.Next(54, 58));    // Extremally Difficult -> max 24 clues (58)
                    break;
            }
        }

        // A method that removes a given number of cells from a Sudoku board while ensuring that the board has a unique solution
        private static void HideNumbers(int numbersToRemove)
        {
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
                    _tempBoard = (int[,])Player!.Board.Clone();
                    Player.HideNumbers.Add(hiddenNumbKey, hiddenNumbTemp);
                    attempts--;
                }
            }
        }

        // A helper method that checks if a given Sudoku board has a unique solution
        private static bool HasUniqueSolution()
        {
            int[,] solutionOne = ConstrainsProg.SolveBoardMin(_tempBoard);
            int[,] solutionTwo = ConstrainsProg.SolveBoardMax(_tempBoard);

            bool isUnique = solutionOne.Rank == solutionTwo!.Rank && Enumerable.Range(0, solutionOne.Rank)
                                .All(dimension => 
                                        solutionOne.GetLength(dimension) == solutionTwo.GetLength(dimension)) && solutionOne.Cast<int>()
                                                   .SequenceEqual(solutionTwo.Cast<int>());

            return isUnique;
        }
    }
}
