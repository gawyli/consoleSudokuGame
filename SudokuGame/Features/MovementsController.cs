using SudokuGame.Models;
using SudokuGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class MovementsController
    {
        public static void Movements(ref int currentRow, ref int currentColumn, ref string cords, PlayerData player)
        {
            // Get user input
            ConsoleKeyInfo playerInput = Console.ReadKey(true);

            #region Movements
            if (playerInput.Key == ConsoleKey.LeftArrow && currentColumn > 0)
            {
                currentColumn--;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.RightArrow && currentColumn < 8)
            {
                currentColumn++;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.UpArrow && currentRow > 0)
            {
                currentRow--;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.DownArrow && currentRow < 8)
            {
                currentRow++;
                cords = currentRow + "," + currentColumn;
            }
            #endregion

            #region Features
            else if (playerInput.Key == ConsoleKey.Z)
            {
                UndoMoveService.UndoMove(player.Board, player.InputNumbers, player.InputNumbersHistory);
            }
            else if (playerInput.Key == ConsoleKey.X)
            {
                RedoMoveService.RedoMove(player.Board, player.InputNumbers, player.InputNumbersHistory);
            }
            else if (playerInput.Key == ConsoleKey.H)
            {
                HintService.AddHint(player.Board, player.HideNumbers, player.InputNumbers);
            }
            else if (playerInput.Key == ConsoleKey.R)
            {
                ResetBoardService.ResetBoard(player.Board, player.HideNumbers, player.InputNumbers, player.InputNumbersHistory);
            }
            else if (playerInput.Key == ConsoleKey.S)
            {
                SaveGameService.SaveGame(player);
            }
            else if (playerInput.Key == ConsoleKey.Q)
            {
                DrawGame.SetOver();
            }
            #endregion

            #region Numbers
            else if (playerInput.Key >= ConsoleKey.D0 && playerInput.Key <= ConsoleKey.D9 && currentRow >= 0 && currentRow <= 8 && currentColumn >= 0 && currentColumn <= 8)
            {
                int value = playerInput.Key - ConsoleKey.D0;    // Convert KeyInfo to int
                if (value == 0)
                {
                    if (player.HideNumbers!.ContainsKey(cords)) // Clear numbers entered by player
                    {
                        player.Board[currentRow, currentColumn] = value; // Swap number entered by player to 0

                        // Enable player to Redo after clearing numbers
                        if (player.InputNumbers.ContainsKey(cords) && player.InputNumbersHistory.ContainsKey(cords))
                        {
                            player.InputNumbersHistory[cords] = player.InputNumbers[cords];
                        }
                        else player.InputNumbersHistory.Add(cords, player.InputNumbers[cords]);
                    }
                }
                else
                {
                    if (player.HideNumbers!.ContainsKey(cords)) // Enable player to enter numbers only to empty cells
                    {
                        player.Board[currentRow, currentColumn] = value; // Update the value
                        
                        // Enable player to do Undo after entered number to the empty cell
                        if (player.InputNumbers.ContainsKey(cords))
                        {
                            player.InputNumbers[cords] = value;
                        }
                        else player.InputNumbers.Add(cords, value);

                    }
                }
            }
            #endregion
        }
    }
}
