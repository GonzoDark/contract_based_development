using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        enum HandType { Rock, Paper, Scissors };
        enum MainMenuSelection { StartGame, ShowRules, EndGame };
        enum SubMenuSelection { NextRound, ShowStatus, GiveUp }; //
        static void Main(string[] args)
        {
            
        int UserInput;
            do
            {
                Console.Clear();
                Console.WriteLine("#Rock Paper Scissors");
                Console.WriteLine("1) Start game");
                Console.WriteLine("2) Show rules");
                Console.WriteLine("3) End game");
                UserInput = int.Parse(Console.ReadLine());
                if (UserInput == (int)MainMenuSelection.StartGame + 1)
                {
                    List<Player> players = definePlayers();
                    defineHand(players);
                    compareHands(players);
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1) Next round");
                        Console.WriteLine("2) Status");
                        UserInput = int.Parse(Console.ReadLine());
                        if (UserInput == (int)SubMenuSelection.NextRound + 1)
                        {
                            defineHand(players);
                            compareHands(players);
                        }
                        if(UserInput == (int)SubMenuSelection.ShowStatus + 1)
                        {
                            ShowStatus(players);
                        }            

                    } while (!hasWonYet(players));
                    Console.Clear();
                    if(players[0].RoundsWon == 3)
                    {
                        Console.WriteLine($"{players[0].Name} won the game!");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine($"{players[1].Name} won the game!");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
                if(UserInput == (int)MainMenuSelection.ShowRules + 1)
                {
                    ShowRules();
                }
            } while (UserInput != (int)MainMenuSelection.EndGame + 1);
        }

        public static void ShowRules()
        {
            Console.Clear();
            Console.WriteLine("#Rules:\n"+"1) Rock beats scissors\n2) Scissors beats paper\n3) Paper beats rock\n"+
                "Win three rounds.\n");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public static void ShowStatus(List<Player> players)
        {
            Console.Clear();
            Console.WriteLine($"#Current score:\n"
                 + $"({players[0].RoundsWon}){players[0].Name}\n"
                 + $"({players[1].RoundsWon}){players[1].Name}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public static List<Player> definePlayers()
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < 2; i++)
            {
                Console.Clear();
                string name;
                Console.WriteLine($"Player {i+1}, enter your name");
                name = Console.ReadLine();
                players.Add(new Player { Name = name });
            }
            return players;
        }

        public static List<Player> defineHand(List<Player> players)
        {

            foreach (Player p in players) //0 --> 1
            {
                Console.Clear();
                Console.WriteLine($"{p.Name} select your hand");
                Console.WriteLine("1) Rock\n2) Paper\n3) Scissor");
                
                p.Hand = int.Parse(Console.ReadLine()) -1;
            }
            return players;
        }

        public static string CompareHandsHelper(List<Player> players)
        {           
            if (players[0].Hand == players[1].Hand)
            {
                return "Draw";
            }

            if (players[0].Hand == (int)HandType.Rock  && players[1].Hand == (int)HandType.Scissors ) // Rock vs Scissors = Rock wins	
            {
                return players[0].Name;
            }

            if (players[0].Hand == (int)HandType.Rock && players[1].Hand == (int)HandType.Paper) // Rock vs Paper = Paper wins
            {
                return players[1].Name;
            }

            if (players[0].Hand == (int)HandType.Scissors  && players[1].Hand == (int)HandType.Rock ) // Scissors vs Rock = Rock wins	
            {
                return players[1].Name;
            }

            if (players[0].Hand == (int)HandType.Scissors  && players[1].Hand == (int)HandType.Paper ) //Scissors vs Paper = Scissors wins	
            {
                return players[0].Name;
            }

            if (players[0].Hand == (int)HandType.Paper  && players[1].Hand == (int)HandType.Scissors ) //Paper vs Scissors = Scissors wins	
            {
                return players[1].Name;
            }

            else // Paper vs Rock = Paper wins
            {
                return players[0].Name;
            }
        }

        public static bool hasWonYet(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.RoundsWon == 3)
                {
                    return true;
                }

            }
            return false;
        }

        public static void compareHands(List<Player> players)
        {
            string winner = CompareHandsHelper(players);
            Console.Clear();
            if (winner == players[0].Name)
            {
                players[0].RoundsWon += 1;
                Console.WriteLine($"{players[0].Name} selected {(HandType)players[0].Hand}\n"
                                 +$"{players[1].Name} selected {(HandType)players[1].Hand}\n");
                Console.WriteLine($"{(HandType)players[0].Hand} beats {(HandType)players[1].Hand}!\n"
                                 +$"{players[0].Name} wins this round!\n");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (winner == players[1].Name)
            {
                players[1].RoundsWon += 1;
                Console.WriteLine($"{players[0].Name} selected {(HandType)players[0].Hand}\n"
                                 + $"{players[1].Name} selected {(HandType)players[1].Hand}\n");
                Console.WriteLine($"{(HandType)players[1].Hand} beats {(HandType)players[0].Hand}!\n"
                                 + $"{players[1].Name} wins this round!\n");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else //if(winner == "Draw")
            {
                Console.WriteLine("Draw!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }
    }
}
