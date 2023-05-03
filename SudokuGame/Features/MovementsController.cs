using SudokuGame.Models;
using SudokuGame.Services;
using SudokuGame.Strategy;
using SudokuGame.Utilities;
using System.Security.Policy;

namespace SudokuGame.Features
{
    public class MovementsController
    {
        public static void Movements(ref int currentRow, ref int currentColumn, ref string cords, ref bool isIndicatorOn, PlayerData player)
        {
            // Get user input
            ConsoleKeyInfo playerInput = Console.ReadKey(true);

            #region Movements
            if (playerInput.Key == ConsoleKey.LeftArrow && currentColumn > 0)
            {
                currentColumn--;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.RightArrow && currentColumn < player.BoardSize - 1)
            {
                currentColumn++;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.UpArrow && currentRow > 0)
            {
                currentRow--;
                cords = currentRow + "," + currentColumn;
            }
            else if (playerInput.Key == ConsoleKey.DownArrow && currentRow < player.BoardSize - 1)
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
                // Minus 3 points
                player.Score -= HintService.AddHint(player.Board, player.HideNumbers, player.InputNumbers);
            }
            else if (playerInput.Key == ConsoleKey.M)
            {
                player.HasUsedIndicator = true;
                isIndicatorOn = MistakeIndicator.Turn(isIndicatorOn);
            }
            else if (playerInput.Key == ConsoleKey.R)
            {
                ResetBoardService.ResetBoard(player.Board, player.HideNumbers, player.InputNumbers, player.InputNumbersHistory);
            }
            else if (playerInput.Key == ConsoleKey.S)
            {
                SaveService.SaveGame(player);
            }
            else if (playerInput.Key == ConsoleKey.P)
            {
                ResetBoardService.ResetBoard(player.Board, player.HideNumbers, player.InputNumbers, player.InputNumbersHistory);
                BenchmarkService.SolverBenchmark(player.Board, player.Level, player.BoardSize);
            }
            else if (playerInput.Key == ConsoleKey.Q)
            {
                DrawGameStrategy.SetOver();
            }
            #endregion

            #region Numbers
            else if (playerInput.Key >= ConsoleKey.D0 && playerInput.Key <= ConsoleKey.D4 && player.BoardSize == 4 ||
                playerInput.Key >= ConsoleKey.D0 && playerInput.Key <= ConsoleKey.D9 && player.BoardSize >= 9 ||
                playerInput.Key >= ConsoleKey.A && playerInput.Key <= ConsoleKey.G && player.BoardSize == 16 &&
                currentRow >= 0 && currentRow < player.BoardSize && 
                currentColumn >= 0 && currentColumn < player.BoardSize)
            {
                int value;
                if (playerInput.Key >= ConsoleKey.A && playerInput.Key <= ConsoleKey.G)
                {
                    value = PlayerUtil.LetterToNumber(playerInput.Key);
                }
                else
                {
                    value = playerInput.Key - ConsoleKey.D0;    // Convert KeyInfo to int
                }

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
