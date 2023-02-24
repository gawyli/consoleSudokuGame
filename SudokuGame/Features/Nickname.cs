using SudokuGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Features
{
    public class Nickname
    {
        private readonly Player _player;

        public Nickname(Player player)
        {
            _player = player;
        }

        public void GetPlayerNickname()
        {
            Console.WriteLine("Enter your nickname: ");
            _player.Nickname = Console.ReadLine()!;

            _player.Id = new Guid();
        }
    }
}
