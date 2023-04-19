namespace SudokuGame.Utilities
{
    public static class PlayerUtil
    {
        public static string LevelAsString(int level)
        {
            if (level == 0) return "Extremally Easy";
            else if (level == 1) return "Easy";
            else if (level == 2) return "Medium";
            else if (level == 3) return "Difficult";
            else if (level == 4) return "Evil";

            return "Error";
        }
        public static string TimeAsString(int time)
        {
            if (time == 0) return "None";
            else if (time == 1) return "10min";
            else if (time == 2) return "15min";
            else if (time == 3) return "20min";
            else if (time == 4) return "25min";

            return "Error";
        }

        public static int TimeInMinutes(int time)
        {
            if (time == 1) return 5000;
            else if (time == 2) return 1500 * 60 * 10;
            else if (time == 3) return 2000 * 60 * 10;
            else if (time == 4) return 2500 * 60 * 10;

            return 0;
        }

        public static bool IsWin(Dictionary<string, int> HideNumbers, Dictionary<string, int> InputNumbers)
        {
            foreach (var key in HideNumbers.Keys)
            {
                if (InputNumbers[key] != HideNumbers[key]) return false;
            }

            return true;
        }

        public static int LetterToNumber(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {
                case ConsoleKey.A:
                    return 10;
                case ConsoleKey.B:
                    return 11;
                case ConsoleKey.C:
                    return 12;
                case ConsoleKey.D: 
                    return 13;
                case ConsoleKey.E:
                    return 14;
                case ConsoleKey.F:
                    return 15;
                case ConsoleKey.G:
                    return 16;
            }

            return 0;
        }
    }
}
