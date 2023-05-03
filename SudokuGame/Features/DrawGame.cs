using SudokuGame.Models;
using SudokuGame.Services;
using SudokuGame.Strategy;
using SudokuGame.Utilities;

namespace SudokuGame.Features
{
    public class DrawGame
    {
        private DrawGameStrategy _strategy = null!;
        private readonly PlayerData _playerData;

        public DrawGame(PlayerData playerData)
        {
            _playerData = playerData;

            switch (_playerData.BoardSize)
            {
                case 4:
                    _strategy = new DrawFourStrategy();
                    break;
                case 9:
                    _strategy = new DrawNineStrategy();
                    break;
                case 16:
                    _strategy = new DrawSixteenStrategy();
                    break;
            }
        }

        public void PopulateSudoku()
        {
            _strategy.PopulateSudoku(_playerData);
        }
    }
}
