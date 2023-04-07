using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Utilities
{
    public class StringUtil
    {
        public static string BoardAsString(int[,] board)
        {
            string boardString = string.Empty;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    boardString += board[i, j].ToString();
                }
            }

            boardString = boardString.Replace("0", ".");

            return boardString;
        }
    }
}
