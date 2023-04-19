using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Utilities
{
    public class BoardUtil
    {
        public static int BoardSizeConverter(int boardSizeOption)
        {
            switch (boardSizeOption)
            {
                case 0:
                    return 4;
                case 1:
                    return 9;
                case 2:
                    return 16;
            }

            return 0;
        }

        public static int InternalBlockSize(int boardSizeOption)
        {
            switch (boardSizeOption)
            {
                case 4:
                    return 2;
                case 9:
                    return 3;
                case 16:
                    return 4;
            }

            return 0;
        }
    }
}
