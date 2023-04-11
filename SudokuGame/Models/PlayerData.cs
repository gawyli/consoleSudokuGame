namespace SudokuGame.Models
{
    /// <summary>
    /// Player data
    /// </summary>

    [Serializable]
    public class PlayerData
    {
        public int Time { get; set; }
        public int Level { get; set; }
        public string? Nickname { get; set; }
        public int[,] Board { get; set; } = new int[9, 9];
        public Dictionary<string, int> HideNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> InputNumbers { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> InputNumbersHistory { get; set; } = new Dictionary<string, int>();
        public int Score { get; set; }
        public bool HasWon { get; set; } = false;
    }
}
