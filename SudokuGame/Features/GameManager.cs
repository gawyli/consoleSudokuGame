using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class GameManager
    {
        static UserConfig userConfig = UserConfig.GetUserConfig();

        public static void StartGame()
        {
            Game.GenerateBoard(userConfig);
            DrawGame.PopulateSudoku(userConfig);
        }

        public static void LoadGame()
        {
            //int[,] loadedGame = new int[9, 9];
            //Game game = new Game(loadedGame);
            //Game.GenerateBoard(userConfig);
        }

        public static void Ranking()
        {
            Console.WriteLine($"Ranking: {userConfig.Nickname} => {userConfig.Score}");
            Console.ReadKey(true);
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
