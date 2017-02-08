using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static WeaponRepository WeaponCollection = new WeaponRepository();
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
                        if (UserInput == (int)SubMenuSelection.ShowStatus + 1)
                        {
                            ShowStatus(players);
                        }

                    } while (!hasWonYet(players));
                    Console.Clear();
                    if (players[0].RoundsWon == 3)
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
                if (UserInput == (int)MainMenuSelection.ShowRules + 1)
                {
                    ShowRules();
                }
            } while (UserInput != (int)MainMenuSelection.EndGame + 1);
        }

        public static void ShowRules()
        {
            Console.Clear();
            Console.WriteLine("#Friends Rules:\n");

            foreach (var weapon in WeaponCollection.Weapons)
            {
                Console.Write($"{weapon.Name} beats ");
                foreach (var weaponBeats in weapon.Beats)
                {
                    if (weaponBeats == weapon.Beats.LastOrDefault())
                    {
                        Console.Write($"{weaponBeats}\n");
                    }
                    else
                    {
                        Console.Write($"{weaponBeats} and ");
                    }
                }
            }
            Console.WriteLine("\nWin three rounds.\n");
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
                Console.WriteLine($"Player {i + 1}, enter your name");
                name = Console.ReadLine();
                players.Add(new Player { Name = name });
            }
            return players;
        }

        public static List<Player> defineHand(List<Player> players)
        {
            foreach (Player p in players)
            {
                Console.Clear();
                Console.WriteLine($"{p.Name} select your hand");

                int counter = 1;
                foreach (Weapon weapon in WeaponCollection.Weapons)
                {
                    Console.WriteLine($"{counter}) {weapon.Name}");
                    counter++;
                }

                p.Hand = WeaponCollection.Weapons[int.Parse(Console.ReadLine()) - 1];
            }
            return players;
        }

        public static string CompareHandsHelper(List<Player> players)
        {
            if (players[0].Hand.Name == players[1].Hand.Name)
            {
                return "Draw";
            }

            foreach (string Weapon in players[0].Hand.Beats)
            {
                if (Weapon == players[1].Hand.Name)
                {
                    return players[0].Name;
                }
            }
            return players[1].Name;
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
                Console.WriteLine($"{players[0].Name} selected {players[0].Hand.Name}\n"
                                 + $"{players[1].Name} selected {players[1].Hand.Name}\n");
                Console.WriteLine($"{players[0].Hand.Name} beats {players[1].Hand.Name}!\n"
                                 + $"{players[0].Name} wins this round!\n");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (winner == players[1].Name)
            {
                players[1].RoundsWon += 1;
                Console.WriteLine($"{players[0].Name} selected {players[0].Hand.Name}\n"
                                 + $"{players[1].Name} selected {players[1].Hand.Name}\n");
                Console.WriteLine($"{players[1].Hand.Name} beats {players[0].Hand.Name}!\n"
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
