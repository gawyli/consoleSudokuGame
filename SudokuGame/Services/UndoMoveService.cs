using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class UndoMoveService
    {
        public static void UndoMove(PlayerData playerData)
        {
            if (playerData.PlayerNumbers.Count > 0)
            {
                string lastPlayerInputCords = playerData.PlayerNumbers.LastOrDefault().Key;
                int lastPlayerInputNumber = playerData.PlayerNumbers[lastPlayerInputCords];

                string[] cords = lastPlayerInputCords.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                playerData.Board[row, col] = 0;


                if (playerData.PlayerNumbersHistory.ContainsKey(lastPlayerInputCords))
                {
                    playerData.PlayerNumbersHistory[lastPlayerInputCords] = lastPlayerInputNumber;
                }
                else playerData.PlayerNumbersHistory.Add(lastPlayerInputCords, lastPlayerInputNumber);

                playerData.PlayerNumbers.Remove(lastPlayerInputCords);
            }
        }
    }
}
