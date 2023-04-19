using SudokuGame.Features;
using SudokuGame.Models;
using SudokuGame.Services;
using SudokuGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Strategy
{
    public class DrawSixteenStrategy : DrawGameStrategy
    {
        public override void PopulateSudoku(PlayerData player)
        {
            int boardSize = player.BoardSize;

            isOver = false;

            while (!isOver)
            {
                // Clear the console after each user move or input
                Console.Clear();

                // Display the grid
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("╓─────────────────╥─────────────────╥─────────────────╥─────────────────╖");
                for (int i = 0; i < boardSize; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("║ ");
                    for (int j = 0; j < boardSize; j++)
                    {
                        string tempCords = i + "," + j;
                        int num = player.Board[i, j];

                        if (i == currentRow && j == currentColumn)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        if (player.Board[i, j] == 0)
                        {
                            Console.Write("    ");
                        }
                        else if (player.HideNumbers.ContainsKey(tempCords))
                        {
                            if (player.Board[i, j] != player.HideNumbers[tempCords])
                            {
                                if (isIndicatorOn)
                                {
                                    // if user input is wrong, it appears red
                                    if (num / 10 == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write($"  {player.Board[i, j]} ");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write($" {player.Board[i, j]} ");
                                    }
                                    
                                }
                                else
                                {
                                    if (num / 10 == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write($"  {player.Board[i, j]} ");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write($" {player.Board[i, j]} ");
                                    } 
                                }
                            }
                            else
                            {
                                if (isIndicatorOn)
                                {
                                    // if user input is valid, it appears green
                                    if (num / 10 == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write($"  {player.Board[i, j]} ");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write($" {player.Board[i, j]} ");
                                    }
                                }
                                else
                                {
                                    if (num / 10 == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write($"  {player.Board[i, j]} ");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write($" {player.Board[i, j]} ");
                                    } 
                                }
                            }
                        }
                        else
                        {                          
                            if (num / 10 == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write($"  {player.Board[i, j]} ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write($" {player.Board[i, j]} ");
                            }  
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        if ((j + 1) % 4 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("║ ");
                        }
                    }
                    Console.WriteLine();
                    if ((i + 1) % 4 == 0 && (i + 1) < boardSize)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("╟─────────────────╫─────────────────╫─────────────────╫─────────────────╢");
                    }
                    else if ((i + 1) == boardSize)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("╙─────────────────╨─────────────────╨─────────────────╨─────────────────╜");
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Level: {PlayerUtil.LevelAsString(player.Level)}");
                Console.WriteLine($"Nickname: {player.Nickname}");
                Console.WriteLine($"Time: {PlayerUtil.TimeAsString(player.Time)}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Use arrow keys to move.\n");
                Console.WriteLine("Z\tUndo");
                Console.WriteLine("X\tRedo");
                Console.WriteLine("H\tHint");

                if (isIndicatorOn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("M\tMistake indicator on");
                }
                else Console.WriteLine("M\tMistake indicator off");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("S\tSave game");
                Console.WriteLine("R\tRestart board");
                Console.WriteLine("Q\tBack to menu\n");
                Console.WriteLine("P\tSolvers benchmark");

                MovementsController.Movements(ref currentRow, ref currentColumn, ref cords, ref isIndicatorOn, player);

                // Check if the game is over
                if (player.HideNumbers.Count == player.InputNumbers.Count)
                {
                    player.HasWon = PlayerUtil.IsWin(player.HideNumbers, player.InputNumbers);

                    if (player.HasWon)
                    {
                        player.Score += RankingService.Calculate(player.Level, player.Time, player.HasUsedIndicator);
                        RankingService.Save(player.Nickname, player.Score);

                        isOver = true;
                    }
                }
            }
        }
    }
}
