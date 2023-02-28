using SudokuGame.Features;
using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Navigation
{
    public class Menu : MenuLogic
    {
        static readonly UserConfig _userConfig = UserConfig.GetUserConfig();
        public static void GameMenu()
        {
            string menuPrompt = "Select:";
            string timePrompt = "Select time constraints:";
            string levelPrompt = "Select level:";
            string[] menuOptions = { "Start game", "Load game", "Ranking", "Exit game" };
            string[] timeOptions = { "None", "10min", "15min", "20min", "25min" };
            string[] levelOptions = { "Easy", "Medium", "Hard" };

            int selectedIndex = RunMenu(menuPrompt, menuOptions);

            switch (selectedIndex)
            {
                case 0:
                    _userConfig.Time = RunMenu(timePrompt, timeOptions);
                    _userConfig.Level = RunMenu(levelPrompt, levelOptions);
                    Console.WriteLine("Enter your username: ");
                    _userConfig.Nickname = Console.ReadLine()!;
                    GameManager.StartGame();
                    break;
                case 1:
                    GameManager.LoadGame();
                    break;
                case 2:
                    GameManager.Ranking();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }   
    }
}
