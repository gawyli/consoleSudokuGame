using Google.OrTools.ConstraintSolver;
using SudokuGame.Models;
using SudokuGame.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;

namespace SudokuGame.Features
{
    public class DrawGame
    {
        private static bool _isOver = false;

        public static void SetOver()
        {
            _isOver = true;
        }

        public static void PopulateSudoku(PlayerData player)
        {
            int currentRow = 0;     //Current position Y
            int currentColumn = 0;  //Current position X
            string cords = currentRow + "," + currentColumn;    // Cords for player's current position

            _isOver = false;

            while (!_isOver)
            {
                if (player.HideNumbers.Count == player.InputNumbers.Count)
                {
                    player.HasWon = PlayerUtil.IsWin(player.HideNumbers, player.InputNumbers);

                    if (player.HasWon) break;
                }

                // Clear the console after each user move or input
                Console.Clear();

                // Display the grid
                Console.WriteLine("+----------+----------+----------+");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write("| ");
                    for (int j = 0; j < 9; j++)
                    {
                        string tempCords = i + "," + j;

                        if (i == currentRow && j == currentColumn)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        if (player.Board[i, j] == 0)
                        {
                            Console.Write("   ");
                        }
                        // if user input is wrong, it appears red
                        else if (player.HideNumbers.ContainsKey(tempCords))
                        {
                            if (player.Board[i, j] != player.HideNumbers[tempCords])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($" {player.Board[i, j]} ");
                            }
                            else // if user input is valid, it appears green
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($" {player.Board[i, j]} ");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($" {player.Board[i, j]} ");
                        }
                            
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        if ((j + 1) % 3 == 0) Console.Write("| ");
                    }
                    Console.WriteLine();
                    if ((i + 1) % 3 == 0) Console.WriteLine("+----------+----------+----------+");
                }

                // Display the level difficulty and key menu
                Console.WriteLine($"Level: {PlayerUtil.LevelAsString(player.Level)}");
                Console.WriteLine($"Nickname: {player.Nickname}");
                Console.WriteLine($"Time: {PlayerUtil.TimeAsString(player.Time)}");
                Console.WriteLine("Use arrow keys to move.\n");
                Console.WriteLine("Z\tUndo");
                Console.WriteLine("X\tRedo");
                Console.WriteLine("H\tHint");
                Console.WriteLine("M\tMistake indicator on/off");
                Console.WriteLine("S\tSave game");
                Console.WriteLine("R\tRestart board");
                Console.WriteLine("Q\tBack to menu\n");

                MovementsController.Movements(ref currentRow, ref currentColumn, ref cords, player);
            }
        }
    }
}
