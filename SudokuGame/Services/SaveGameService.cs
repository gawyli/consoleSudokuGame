using SudokuGame.Models;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace SudokuGame.Services
{
    public class SaveGameService : DirectoryBase
    {
        public static void SaveGame(PlayerData player)
        {
            Stopwatch st = new Stopwatch(); // Benchmark

            string gameName = player.Nickname + DateTime.UtcNow.ToString("ddMMyy-HHmmss") + ".bin";
            string filePath = Path.Combine(dir, gameName);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            st.Start();
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, player);
            }
            st.Stop();

            Console.WriteLine("Stopwatch: " + st.ElapsedMilliseconds + "ms");
            Console.WriteLine("Game has been saved! Press key to continue..");
            Console.ReadKey(true);
        }
    }
}
