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
            if (playerData.InputNumbers.Count > 0)
            {
                string lastPlayerInputCords = playerData.InputNumbers.LastOrDefault().Key;
                int lastPlayerInputNumber = playerData.InputNumbers[lastPlayerInputCords];

                string[] cords = lastPlayerInputCords.Split(',');
                int row = int.Parse(cords[0]);
                int col = int.Parse(cords[1]);

                playerData.Board[row, col] = 0;


                if (playerData.InputNumbersHistory.ContainsKey(lastPlayerInputCords))
                {
                    playerData.InputNumbersHistory[lastPlayerInputCords] = lastPlayerInputNumber;
                }
                else playerData.InputNumbersHistory.Add(lastPlayerInputCords, lastPlayerInputNumber);

                playerData.InputNumbers.Remove(lastPlayerInputCords);
            }
        }
    }
}
