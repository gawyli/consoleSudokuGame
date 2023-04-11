using SudokuGame.Models;
using SudokuGame.Utilities;


namespace SudokuGame.Features
{
    public class GameManager
    {
        public static void StartGame(PlayerData player)
        {
            bool isReady = false;

            while (!isReady)
            {
                Console.Clear();
                Console.WriteLine("Loading...");

                isReady = Game.NewGame(player);
            }

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
