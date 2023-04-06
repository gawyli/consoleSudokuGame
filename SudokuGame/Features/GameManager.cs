using SudokuGame.Models;
using SudokuGame.New;
using SudokuGame.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace SudokuGame.Features
{
    public class GameManager
    {
        public static void StartGame(PlayerData player)
        {
            _ = new Game(player);

            if (player.Time > 0)
            {
                EndGame.SetTimer(player.TimeInMinutes()); 
            }

            DrawGame.PopulateSudoku(player);

            if (DrawGame.EndGame())
            {
                EndGame.GameOver();
            }
        }

        public static void LoadGame(PlayerData player)
        {
            
            DrawGame.PopulateSudoku(player);
        }

        public static void Ranking()
        {
            Console.WriteLine($"Ranking: ");
            Console.ReadKey(true);
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
