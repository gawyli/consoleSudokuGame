using SudokuGame.Models;
using SudokuGame.New;
using SudokuGame.Services;
using SudokuGame.Utilities;
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
            Game.NewGame(player);

            if (player.Time > 0)
            {
                EndGame.SetTimer(PlayerUtil.TimeInMinutes(player.Time)); 
            }

            DrawGame.PopulateSudoku(player);

            EndGame.Over(player.HasWon);
        }

        public static void LoadGame(PlayerData player)
        {
            DrawGame.PopulateSudoku(player);

            EndGame.Over(player.HasWon);
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
