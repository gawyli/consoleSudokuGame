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
        private static bool over = false;

        public static void StartGame()
        {
            Game.GenerateTheBoard(userConfig);

            do
            {
                DrawGame.PopulateSudoku(userConfig.Board);

                string usInp = Console.ReadLine();

                if (usInp == "t")
                {
                    over = true;
                };


            } while (!over);  
        }

        public static void LoadGame()
        {
            int[,] loadedGame = new int[9, 9];
            Game game = new Game(loadedGame);
            Game.GenerateTheBoard(userConfig);
        }

        public static void Ranking()
        {
            Console.WriteLine($"Ranking: {userConfig.Nickname} => {userConfig.Score}");
            Console.ReadKey(true);
        }
    }
}
