using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class DrawGame
    {
        public static void PopulateSudoku(UserConfig userCfg)
        {
            int currentRow = 0;     //Current position Y
            int currentColumn = 0;  //Current position X
            string cords = currentRow + "," + currentColumn;    // Cords for player's current position

            bool over = false;
            
            while (!over)
            {   
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
                        if (userCfg.Board[i, j] == 0)
                        {
                            Console.Write("   ");
                        }
                        // if user input is wrong, it appears red
                        else if (userCfg.HidingNumbers.ContainsKey(tempCords))
                        {
                            if (userCfg.Board[i, j] != userCfg.HidingNumbers[tempCords])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($" {userCfg.Board[i, j]} ");
                            }
                            else // if user input is valid, it appears green
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($" {userCfg.Board[i, j]} ");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($" {userCfg.Board[i, j]} ");
                        }
                            
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        if ((j + 1) % 3 == 0) Console.Write("| ");
                    }
                    Console.WriteLine();
                    if ((i + 1) % 3 == 0) Console.WriteLine("+----------+----------+----------+");
                }

                // Display the level difficulty and key menu
                Console.WriteLine($"Level: {userCfg.LevelAsString()}");
                Console.WriteLine($"Nickname: {userCfg.Nickname}");
                Console.WriteLine("Use arrow keys to move, 0-9 to set value.\n");
                Console.WriteLine("Z\tUndo");
                Console.WriteLine("X\tRedo");
                Console.WriteLine("H\tHint");
                Console.WriteLine("M\tMistake indicator on/off");
                Console.WriteLine("S\tSave game");
                Console.WriteLine("R\tRestart board");
                Console.WriteLine("Q\tBack to menu\n");


                MovementsController.Movements(ref currentRow, ref currentColumn, ref over, ref cords, userCfg);
            }
        }
    }
}
