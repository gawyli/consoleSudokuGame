namespace SudokuGame.Models
{
    /// <summary>
    /// Player data
    /// </summary>

    [Serializable]
    public class PlayerData
    {
        public int BoardSize { get; set; } = 9;
        public int Time { get; set; }
        public int Level { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int[,] Board { get; set; } = null!;
        public Dictionary<string, int> HideNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> InputNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> InputNumbersHistory { get; set; } = new Dictionary<string, int>();
        public int Score { get; set; }
        public bool HasWon { get; set; } = false;
        public bool HasUsedIndicator { get; set; } = false;

        //public PlayerData()
        //{
        //    Board = new int[BoardSize, BoardSize];
        //}

        public PlayerData(int boardSize) 
        {
            BoardSize = boardSize;
            Board = new int[BoardSize, BoardSize];
        }
    }
}
