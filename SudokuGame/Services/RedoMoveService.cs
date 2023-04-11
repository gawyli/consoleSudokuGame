using SudokuGame.Utilities;

namespace SudokuGame.Services
{
    public class RedoMoveService
    {
        public static void RedoMove(int[,] board,
            Dictionary<string, int> inputNumbers,
            Dictionary<string, int> inputNumbersHistory)
        {
            if (inputNumbersHistory.Count > 0)
            {
                int row, col;

                string lastUndoInputCords = inputNumbersHistory.LastOrDefault().Key;
                int lastUndoInputNumber = inputNumbersHistory[lastUndoInputCords];

                CordsUtil.GetCords(lastUndoInputCords, out row, out col);

                board[row, col] = lastUndoInputNumber;

                if (inputNumbers.ContainsKey(lastUndoInputCords))
                {
                    board[row, col] = lastUndoInputNumber;
                }
                else inputNumbers.Add(lastUndoInputCords, lastUndoInputNumber);
                inputNumbersHistory.Remove(lastUndoInputCords);
            }
        }
    }
}
