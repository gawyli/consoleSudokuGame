using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Models
{
    public class UserConfig
    {
        public int Time { get; set; }
        public int Level { get; set; } 
        public string Nickname { get; set; } = null!;
        public int[,] Board { get; set; } = null!;
        public Dictionary<string, int> HidingNumbers { get; set; }
        public int Score { get; set; }

        static UserConfig? userConfigIstance;
        private static object locker = new object();

        protected UserConfig()
        {

        }

        public static UserConfig GetUserConfig()
        {
           if (userConfigIstance == null)
           {
                lock (locker)
                {
                    if (userConfigIstance == null)
                    {
                        userConfigIstance = new UserConfig();
                    }
                }
           }

           return userConfigIstance;
        }

        public string LevelAsString()
        {
            if (Level == 0) return "Easy";
            else if (Level == 1) return "Medium";
            else if (Level == 2) return "Hard";

            return "Error";
        }
    }
}
