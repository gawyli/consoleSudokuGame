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
            if (playerData.InputNumbersHistory.Count > 0)
            {
                string lastUndoInputCords = playerData.InputNumbersHistory.LastOrDefault().Key;
                int lastUndoInputNumber = playerData.InputNumbersHistory[lastUndoInputCords];

                string[] cords = lastUndoInputCords.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                playerData.Board[row, col] = lastUndoInputNumber;

                if (playerData.InputNumbers.ContainsKey(lastUndoInputCords))
                {
                    playerData.Board[row, col] = lastUndoInputNumber;
                }
                else playerData.InputNumbers.Add(lastUndoInputCords, lastUndoInputNumber);
                playerData.InputNumbersHistory.Remove(lastUndoInputCords);
            }
        }
    }
}
