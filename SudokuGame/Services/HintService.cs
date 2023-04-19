using SudokuGame.Utilities;

namespace SudokuGame.Services
{
    public class HintService
    {
        public static int AddHint(int[,] board,
            Dictionary<string, int> hideNumbers,
            Dictionary<string, int> inputNumbers)
        {
            int row, col;

            Random random = new Random();
            int keysLength = hideNumbers.Count;
            var keyValuePair = hideNumbers.ElementAt(random.Next(keysLength));

            if (inputNumbers.Count <= hideNumbers.Count)
            {
                while (inputNumbers.ContainsKey(keyValuePair.Key))
                {
                    if (inputNumbers[keyValuePair.Key] == keyValuePair.Value)
                    {
                        keyValuePair = hideNumbers.ElementAt(random.Next(keysLength));
                    }
                    else
                    {
                        break;
                    }
                }

                CordsUtil.GetCords(keyValuePair.Key, out row, out col);

                board[row, col] = keyValuePair.Value;
                inputNumbers[keyValuePair.Key] = keyValuePair.Value;
            }

            return 3;
        }
    }
}
