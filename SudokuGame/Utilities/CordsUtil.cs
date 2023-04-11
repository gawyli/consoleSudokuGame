namespace SudokuGame.Utilities
{
    public class CordsUtil
    {
        public static void GetCords(string key, out int row, out int col)
        {
            string[] cords = key.Split(',');
            row = int.Parse(cords[0]);
            col = int.Parse(cords[1]);
        }
    }
}
