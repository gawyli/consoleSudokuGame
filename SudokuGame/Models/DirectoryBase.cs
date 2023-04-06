using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Models
{
    public class DirectoryBase
    {
        public static string directory = "/SaveGame/";
        public static string dir = AppContext.BaseDirectory + directory;

    }
}
