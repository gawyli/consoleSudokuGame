using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Models
{
    /// <summary>
    /// Player data
    /// </summary>

    [Serializable]
    public class PlayerData
    {
        public int Time { get; set; }
        public int Level { get; set; } 
        public string? PlayerName { get; set; }
        public int[,] Board { get; set; } = new int[9, 9];
        public Dictionary<string, int> HidingNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> PlayerNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> PlayerNumbersHistory { get; set; } = new Dictionary<string, int>();
        public int Score { get; set; }

        public string LevelAsString()
        {
            if (Level == 0) return "Extremally Easy";
            else if (Level == 1) return "Easy";
            else if (Level == 2) return "Medium";
            else if (Level == 3) return "Difficult";
            else if (Level == 4) return "Evil";

            return "Error";
        }
        public string TimeAsString()
        {
            if (Time == 0) return "None";
            else if (Time == 1) return "10min";
            else if (Time == 2) return "15min";
            else if (Time == 3) return "20min";
            else if (Time == 4) return "25min";

            return "Error";
        }

        public int TimeInMinutes()
        {
            if (Time == 1) return 5000;
            else if (Time == 2) return 1500 * 60 * 10;
            else if (Time == 3) return 2000 * 60 * 10;
            else if (Time == 4) return 2500 * 60 * 10;

            return 0;
        }
    }
}
