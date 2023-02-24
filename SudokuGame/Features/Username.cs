using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class Username
    {
        private readonly Player _player;

        public Username(Player player)
        {
            _player = player;
        }

        public void GetPlayerNickname()
        {
            Console.WriteLine("Enter your nickname: ");
            _player.Username = Console.ReadLine()!;

            _player.Id = new Guid();
        }
    }
}
