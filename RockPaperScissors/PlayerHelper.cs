using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class PlayerHelper
    {
        public static List<Match> tournamentHandler(int amountPlayers, List<Player> players)
        {           
            List<Match> Tournament = new List<Match>();
            for (int i = 0; i < amountPlayers; i++)
            {
                for (int j = i; j < amountPlayers; j++)
                {
                    if (i != j)
                    {
                        Match newRound = new Match();
                        newRound.Players = new List<Player>();
                        newRound.Players.Add(players[i]);
                        newRound.Players.Add(players[j]);
                        Tournament.Add(newRound);
                    }
                }
            }
            return Tournament;        
        }
    }
}
