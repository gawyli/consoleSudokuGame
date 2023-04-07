using SudokuGame.Models;
using SudokuGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class ResetBoardService
    {
        public static void ResetBoard(int[,] board, 
            Dictionary<string, int> hideNumbers, 
            Dictionary<string, int> inputNumbers, 
            Dictionary<string, int> inputNumbersHistory)
        {
            foreach (var key in hideNumbers.Keys)
            {
                int row, col;

                CordsUtil.GetCords(key, out row, out col);

                board[row, col] = 0;

                inputNumbers.Clear();
                inputNumbersHistory.Clear();
            }
        }
    }
}
