using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Strategy
{
    public abstract class DrawGameStrategy
    {
        protected static bool isOver = false;

        protected static int currentRow = 0;     // Current position Y
        protected static int currentColumn = 0;  // Current position X
        protected static string cords = currentRow + "," + currentColumn;    // Cords for player's current position

        protected static bool isIndicatorOn = false;

        public static void SetOver()
        {
            isOver = true;
        } 

        public abstract void PopulateSudoku(PlayerData player);
    }
}
