namespace RockPaperScissors
{
    public class Player
    {
        public int id { get; set; } // just for fun, so I can see the combinations
        public string Name { get; set; }
        public Weapon Hand { get; set; }
        public int RoundsWon { get; set; }
        public int MatchesWon { get; set; } 
    }
}