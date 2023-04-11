using SudokuGame.Utilities;

namespace SudokuGame.Services
{
    public class UndoMoveService
    {
        public static void UndoMove(int[,] board,
            Dictionary<string, int> inputNumbers,
            Dictionary<string, int> inputNumbersHistory)
        {
            if (inputNumbers.Count > 0)
            {
                int row, col;

                string lastPlayerInputCords = inputNumbers.LastOrDefault().Key;
                int lastPlayerInputNumber = inputNumbers[lastPlayerInputCords];

                CordsUtil.GetCords(lastPlayerInputCords, out row, out col);

                board[row, col] = 0;

                if (inputNumbersHistory.ContainsKey(lastPlayerInputCords))
                {
                    inputNumbersHistory[lastPlayerInputCords] = lastPlayerInputNumber;
                }
                else inputNumbersHistory.Add(lastPlayerInputCords, lastPlayerInputNumber);

                inputNumbers.Remove(lastPlayerInputCords);
            }
        }
    }
}
