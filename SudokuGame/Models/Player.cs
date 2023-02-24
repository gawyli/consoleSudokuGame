using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
    }
}
