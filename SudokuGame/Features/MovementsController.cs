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
        public static void Movements(ref int currentRow, ref int currentColumn, ref bool _over, ref string cords, PlayerData playerData)
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

            #region Submenu
            else if (playerInput.Key == ConsoleKey.Z)
            {
                UndoMoveService.UndoMove(playerData);
            }
            else if (playerInput.Key == ConsoleKey.X)
            {
                RedoMoveService.RedoMove(playerData);
            }
            else if (playerInput.Key == ConsoleKey.R)
            {
                Game.ResetBoard();
            }
            else if (playerInput.Key == ConsoleKey.S)
            {
                SaveGameService.SaveGame(playerData);
            }
            else if (playerInput.Key == ConsoleKey.Q)
            {
                _over = true;
            }

            #endregion

            #region Numbers
            else if (playerInput.Key >= ConsoleKey.D0 && playerInput.Key <= ConsoleKey.D9 && currentRow >= 0 && currentRow <= 8 && currentColumn >= 0 && currentColumn <= 8)
            {
                int value = playerInput.Key - ConsoleKey.D0;    // Convert KeyInfo to int
                if (value == 0)
                {
                    if (playerData.HidingNumbers!.ContainsKey(cords)) // Clear numbers entered by player
                    {
                        playerData.Board[currentRow, currentColumn] = value; // Swap number entered by player to 0

                        // Enable player to Redo after clearing numbers
                        if (playerData.PlayerNumbers.ContainsKey(cords) && playerData.PlayerNumbersHistory.ContainsKey(cords))
                        {
                            playerData.PlayerNumbersHistory[cords] = playerData.PlayerNumbers[cords];
                        }
                        else playerData.PlayerNumbersHistory.Add(cords, playerData.PlayerNumbers[cords]);
                    }
                }
                else
                {
                    if (playerData.HidingNumbers!.ContainsKey(cords)) // Enable player to enter numbers only to empty cells
                    {
                        playerData.Board[currentRow, currentColumn] = value; // Update the value
                        
                        // Enable player to do Undo after entered number to the empty cell
                        if (playerData.PlayerNumbers.ContainsKey(cords))
                        {
                            playerData.PlayerNumbers[cords] = value;
                        }
                        else playerData.PlayerNumbers.Add(cords, value);

                    }
                }
            }
            #endregion
        }
    }
}
