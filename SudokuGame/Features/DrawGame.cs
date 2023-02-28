using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class DrawGame
    {
        public static void PopulateSudoku(int[,] board)   // Draw the board
        {
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Time execution = " + elapsedMs.ToString() + "ms" + "\n");


            int isColThree = 0, isRowThree = 0;

            for (int i = 0; i < 9; i++)
            {
                isRowThree++;

                for (int j = 0; j < 9; j++)
                {
                    Console.Write(" " + board[i, j].ToString() + " ");
                    isColThree++;

                    if (isColThree == 3)
                    {
                        Console.Write(" | ");
                        isColThree = 0;
                    }
                }
                Console.WriteLine();

                if (isRowThree == 3)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        Console.Write(" - ");
                    }
                    Console.WriteLine();

                    isRowThree = 0;
                }
            }

            int total = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        total++;
                    }
                }
            }
            int emptyCells = 81 - total;

            Console.WriteLine("\n" + "Total: " + emptyCells.ToString());
        }
    }
}
