using SudokuGame.Models;
using SudokuGame.Services;
using SudokuGame.Utilities;


namespace SudokuGame.Features
{
    public class GameManager
    {
        private static DrawGame drawGame = null!;
        public static void StartGame(PlayerData player)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            Game.NewGame(player);

            if (player.Time > 0)
            {
                EndGame.SetTimer(PlayerUtil.TimeInMinutes(player.Time));
            }

            drawGame = new DrawGame(player);
            drawGame.PopulateSudoku();

            EndGame.Over(player.HasWon, player.Score);
        }

        public static void LoadGame(PlayerData player)
        {
            drawGame = new DrawGame(player);
            drawGame.PopulateSudoku();
            EndGame.Over(player.HasWon, player.Score);
        }

        public static void Ranking()
        {
            RankingService.Load();
            Console.ReadKey(true);
        }

        public static void Instructions()
        {
            Console.WriteLine("\nInstructions:");
            Console.WriteLine(@"Board 4x4:
    A 4×4 square must be filled in with numbers from 1-4 
    with no repeated numbers in each line, 
    horizontally or vertically. To challenge you more, 
    there are 2×2 squares marked out in the grid,
    and each of these squares can't have any repeat numbers either.");

            Console.WriteLine(@"Board 9x9:
    A 9×9 square must be filled in with numbers from 1-9 
    with no repeated numbers in each line, 
    horizontally or vertically. To challenge you more, 
    there are 3×3 squares marked out in the grid,
    and each of these squares can't have any repeat numbers either.");

            Console.WriteLine(@"Board 16x16:
    !IMPORTANT!
    1. In some cases generating a board can takes up to 40 second!
    2. Numbers 10 - 16 are represented by key letters A - G!

    A 16×16 square must be filled in with numbers from 1-16 
    with no repeated numbers in each line, 
    horizontally or vertically. To challenge you more, 
    there are 4×4 squares marked out in the grid,
    and each of these squares can't have any repeat numbers either.");
            Console.ReadKey(true);
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
