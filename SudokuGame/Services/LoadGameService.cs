using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class LoadGameService : DirectoryBase
    {
        public static PlayerData GetGameFromFile(string fileName)
        {
            string filePath = Path.Combine(dir, fileName);
            var player = new PlayerData();

            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                player = (PlayerData)binaryFormatter.Deserialize(stream);
            }

            return player;
        }

        public static string[] GetSavedGameNames()
        {
            var directory = new DirectoryInfo(dir);

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
