using SudokuGame.Features;
using SudokuGame.Models;
using SudokuGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Navigation
{
    public class Menu : MenuLogic
    {
        public static void GameMenu()
        {
            var player = new PlayerData();

            string menuPrompt = "Select:";
            string timePrompt = "Select time constraints:";
            string levelPrompt = "Select level:";
            string loadGamePrompt = "Select saved game:";

            string enterNicknamePrompt = "Enter your nickname:";

            string[] menuOptions = { "Start game", "Load game", "Ranking", "Exit game" };
            string[] timeOptions = { "None", "10min", "15min", "20min", "25min" };
            string[] levelOptions = { "Extremely Easy", "Easy", "Medium", "Difficult", "Evil" };

            int selectedIndex = RunMenu(menuPrompt, menuOptions);

            switch (selectedIndex)
            {
                case 0:
                    player.Time = RunMenu(timePrompt, timeOptions);
                    player.Level = RunMenu(levelPrompt, levelOptions);
                    player.Nickname = RunEnterField(enterNicknamePrompt);
                    GameManager.StartGame(player);
                    break;
                case 1:
                    string[] listGames = LoadGameService.GetSavedGameNames();
                    int fileNameIndex = RunMenu(loadGamePrompt, listGames);
                    player = LoadGameService.GetGameFromFile(listGames[fileNameIndex]);
                    GameManager.LoadGame(player);
                    break;
                case 2:
                    GameManager.Ranking();
                    break;
                case 3:
                    GameManager.Exit();
                    break;
            }
        }   
    }
}
