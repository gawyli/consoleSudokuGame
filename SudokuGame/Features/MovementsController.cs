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
        public static void Movements(ref int currentRow, ref int currentColumn, ref bool over, ref string cords, UserConfig userCfg)
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
            else if (playerInput.Key == ConsoleKey.R)
            {
                Game.ResetBoard();
            }
            else if (playerInput.Key == ConsoleKey.R)
            {
                Game.ResetBoard();
            }
            else if (playerInput.Key == ConsoleKey.S)
            {
                SaveGameService.SaveGame();
            }
            else if (playerInput.Key == ConsoleKey.Q)
            {
                over = true;
            }

            #endregion

            #region Numbers
            else if (playerInput.Key >= ConsoleKey.D0 && playerInput.Key <= ConsoleKey.D9 && currentRow >= 0 && currentRow <= 8 && currentColumn >= 0 && currentColumn <= 8)
            {
                int value = playerInput.Key - ConsoleKey.D0;
                if (value == 0)
                {
                    if (userCfg.HidingNumbers.ContainsKey(cords)) // Only clear if cell is not green
                    {
                        userCfg.Board[currentRow, currentColumn] = 0; // Clear the value
                    }
                }
                else
                {
                    if (userCfg.HidingNumbers.ContainsKey(cords)) // Only update if cell is not green
                    {
                        userCfg.Board[currentRow, currentColumn] = value; // Update the value
                    }
                }
            }
            #endregion
        }
    }
}
