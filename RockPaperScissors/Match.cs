using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Match
    {
        public List<Player> Players { get; set; }
        public int roundNumber { get; set; } // could use this to limit the player to only 5 rounds, but nah.
        public Player Winner { get; set; }
        public Player Loser { get; set; }
    }
}
