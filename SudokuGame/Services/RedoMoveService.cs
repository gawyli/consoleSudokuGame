using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class RedoMoveService
    {
        public static void RedoMove(PlayerData playerData)
        {
            if (playerData.PlayerNumbersHistory.Count > 0)
            {
                string lastUndoInputCords = playerData.PlayerNumbersHistory.LastOrDefault().Key;
                int lastUndoInputNumber = playerData.PlayerNumbersHistory[lastUndoInputCords];

                string[] cords = lastUndoInputCords.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                playerData.Board[row, col] = lastUndoInputNumber;

                if (playerData.PlayerNumbers.ContainsKey(lastUndoInputCords))
                {
                    playerData.Board[row, col] = lastUndoInputNumber;
                }
                else playerData.PlayerNumbers.Add(lastUndoInputCords, lastUndoInputNumber);
                playerData.PlayerNumbersHistory.Remove(lastUndoInputCords);
            }
        }
    }
}
