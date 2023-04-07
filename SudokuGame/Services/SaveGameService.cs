using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Enumeration;
using SudokuGame.Models;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Diagnostics;

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
