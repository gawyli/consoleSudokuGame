using Google.OrTools.ConstraintSolver;
using SudokuGame.Models;
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

        public static void Timeout()
        {
            var sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            _isOver = true;
        }

        private static void IsOver(int hiddenNumbersLength, int playerInputLength)
        {
            if (hiddenNumbersLength == playerInputLength)
            {
                _isOver = true;
            }
        }

        public static bool EndGame()
        {
            return _isOver;
        }

        public static void PopulateSudoku(PlayerData playerData)
        {
            int currentRow = 0;     //Current position Y
            int currentColumn = 0;  //Current position X
            string cords = currentRow + "," + currentColumn;    // Cords for player's current position

            _isOver = false;

            while (!_isOver)
            {
                IsOver(playerData.HidingNumbers.Count, playerData.PlayerNumbers.Count);

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
                        if (playerData.Board[i, j] == 0)
                        {
                            Console.Write("   ");
                        }
                        // if user input is wrong, it appears red
                        else if (playerData.HidingNumbers.ContainsKey(tempCords))
                        {
                            if (playerData.Board[i, j] != playerData.HidingNumbers[tempCords])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($" {playerData.Board[i, j]} ");
                            }
                            else // if user input is valid, it appears green
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($" {playerData.Board[i, j]} ");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($" {playerData.Board[i, j]} ");
                        }
                            
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        if ((j + 1) % 3 == 0) Console.Write("| ");
                    }
                    Console.WriteLine();
                    if ((i + 1) % 3 == 0) Console.WriteLine("+----------+----------+----------+");
                }

                // Display the level difficulty and key menu
                Console.WriteLine($"Level: {playerData.LevelAsString()}");
                Console.WriteLine($"Nickname: {playerData.PlayerName}");
                Console.WriteLine($"Time: {playerData.TimeAsString()}");
                Console.WriteLine("Use arrow keys to move, 0-9 to set value.\n");
                Console.WriteLine("Z\tUndo");
                Console.WriteLine("X\tRedo");
                Console.WriteLine("H\tHint");
                Console.WriteLine("M\tMistake indicator on/off");
                Console.WriteLine("S\tSave game");
                Console.WriteLine("R\tRestart board");
                Console.WriteLine("Q\tBack to menu\n");

                MovementsController.Movements(ref currentRow, ref currentColumn, ref _isOver, ref cords, playerData);
            }
        }
    }
}
