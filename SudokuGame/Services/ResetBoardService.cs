using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class ResetBoardService
    {
        public static void ResetBoard(PlayerData player)
        {
            foreach (var key in player.HideNumbers.Keys)
            {
                string[] cords = key.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                player.Board[row, col] = 0;

                player.InputNumbers.Clear();
                player.InputNumbersHistory.Clear();
            }
        }
    }
}
