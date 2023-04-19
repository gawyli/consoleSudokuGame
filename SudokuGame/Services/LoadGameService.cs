using SudokuGame.Models;

namespace SudokuGame.Services
{
    public class LoadGameService : DirectoryBase
    {
        public static PlayerData GetGameFromFile(string gameName)
        {
            string filePath = Path.Combine(sDir, gameName);
            PlayerData player;

            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                player = (PlayerData)binaryFormatter.Deserialize(stream);
            }

            return player;
        }

        public static string[] GetSavedGameNames()
        {
            var directory = new DirectoryInfo(sDir);

            FileInfo[] files = directory.GetFiles("*.bin");
            string[] gameSaveList = new string[files.Length];
            int counter = 0;

            foreach (var file in files)
            {
                gameSaveList[counter] += file.Name;
                counter++;
            }

            return gameSaveList;
        }
    }
}
