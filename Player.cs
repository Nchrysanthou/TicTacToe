using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player
    {
        public string Name { get; set; }
        public bool IsPlayer { get; set; }

        public Player(string Name, bool IsPlayer)
        {
            this.Name = Name;
            this.IsPlayer = IsPlayer;
        }


    }
}
