namespace SudokuGame.Models
{
    public class DirectoryBase
    {
        public static string saveGameDirectory = "/SaveGame/";
        public static string rankingDirectory = "/Ranking/";
        public static string sDir = AppContext.BaseDirectory + saveGameDirectory;
        public static string rDir = AppContext.BaseDirectory + rankingDirectory;
    }
}
