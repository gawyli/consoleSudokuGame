using SudokuGame.Features;
using SudokuGame.Models;
using SudokuGame.Services;
using SudokuGame.Utilities;

namespace SudokuGame.Navigation
{
    public class Menu : MenuLogic
    {
        public static void GameMenu()
        {
            PlayerData player;

            string menuPrompt = "Select:";
            string timePrompt = "Select time constraints:";
            string levelPrompt = "Select level:";
            string loadGamePrompt = "Select saved game:";

            // Experimental
            string boardSizePrompt = "Select board size:";
            string[] boardSizeOptions = { "4x4", "9x9", "16x16" };

            string enterNicknamePrompt = "Enter your nickname:";

            string[] menuOptions = { "Start game", "Load game", "Ranking", "Instructions", "Exit game" };
            string[] timeOptions = { "None", "10min", "15min", "20min", "25min" };
            string[] levelOptions = { "Extremely Easy", "Easy", "Medium", "Difficult", "Evil" };

            int selectedIndex = RunMenu(menuPrompt, menuOptions);

            switch (selectedIndex)
            {
                case 0:
                    Settings.BOARD_SIZE = BoardUtil.BoardSizeConverter(RunMenu(boardSizePrompt, boardSizeOptions));
                    player = new PlayerData(Settings.BOARD_SIZE);
                    player.Level = RunMenu(levelPrompt, levelOptions);
                    player.Time = RunMenu(timePrompt, timeOptions);
                    player.Nickname = RunEnterField(enterNicknamePrompt);
                    GameManager.StartGame(player);
                    break;
                case 1:
                    string[] listGames = LoadGameService.GetSavedGameNames();
                    if (listGames.Length < 1)
                        break;
                    int fileNameIndex = RunMenu(loadGamePrompt, listGames);
                    player = LoadGameService.GetGameFromFile(listGames[fileNameIndex]);
                    GameManager.LoadGame(player);
                    break;
                case 2:
                    GameManager.Ranking();
                    break;
                case 3:
                    GameManager.Instructions();
                    break;
                case 4:
                    GameManager.Exit();
                    break;
            }
        }
    }
}
