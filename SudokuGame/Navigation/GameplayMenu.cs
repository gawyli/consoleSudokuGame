using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Navigation
{
    public class GameplayMenu : MenuLogic
    {
        public static void DisplayGameplayMenu()
        {
            string prop1 = "Select:";
            string prop2 = "Select time constraints:";
            string prop3 = "Select level:";
            string[] opt1 = { "Start game", "Load game", "Ranking", "Exit game" };
            string[] opt2 = { "None", "10min", "15min", "20min", "25min" };
            string[] opt3 = { "Easy", "Medium", "Hard" };

            int selectedIndex = RunMenu(prop1, opt1);

            switch (selectedIndex)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
        }
    }
}
