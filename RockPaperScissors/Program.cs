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
        enum MainMenuSelection { StartTournament, QuickMatch, ShowRules, EndGame };
        enum SubMenuSelection { NextRound, ShowStatus, GiveUp }; //
        static void Main(string[] args)
        {
            int UserInput;
            do
            {
                Console.Clear();
                Console.WriteLine("#Rock Paper Scissors");
                Console.WriteLine("1) Start tournament");
                Console.WriteLine("2) Quick round"); // could make this into a quick match fairly easy
                Console.WriteLine("3) Show rules");
                Console.WriteLine("4) End game");
                UserInput = int.Parse(Console.ReadLine());
                if (UserInput == (int)MainMenuSelection.StartTournament + 1) //should make methods for this
                {
                    Console.Clear();
                    Console.WriteLine("How many players are playing?");
                    UserInput = int.Parse(Console.ReadLine());
                    List<Player> players = definePlayers(UserInput);
                    List<Match> tournament = PlayerHelper.tournamentHandler(UserInput,players);

                    foreach (var match in tournament)
                    {
                        defineHand(match.Players);
                        compareHands(match.Players);
                        // newGame(players);

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("1) Next round");
                            Console.WriteLine("2) Status");
                            UserInput = int.Parse(Console.ReadLine());
                            if (UserInput == (int)SubMenuSelection.NextRound + 1)
                            {
                                defineHand(match.Players);
                                compareHands(match.Players);
                            }
                            if (UserInput == (int)SubMenuSelection.ShowStatus + 1)
                            {
                                ShowStatus(match.Players);
                            }

                        } while (!hasWonYet(match.Players));
                        Console.Clear();
                        if (match.Players[0].RoundsWon == 3)
                        {
                            Console.WriteLine($"{match.Players[0].Name} won the match!");
                            match.Winner = match.Players[0];
                            match.Loser = match.Players[1];

                            match.Players[0].RoundsWon = 0; // reset game
                            match.Players[1].RoundsWon = 0;
                            match.Players[0].MatchesWon += 1;
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"{match.Players[1].Name} won the match!");
                            match.Winner = match.Players[1];
                            match.Loser = match.Players[0];
                            match.Players[0].RoundsWon = 0;
                            match.Players[1].RoundsWon = 0;
                            match.Players[1].MatchesWon += 1;
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                        }
                    }

                    int max = 0;
                    Player TournamentWinner = new Player();
                    foreach (var match in tournament)
                    {
                        if(match.Players[0].MatchesWon > max)
                        {
                            max = match.Players[0].MatchesWon;
                            TournamentWinner = match.Players[0];
                        }
                        if (match.Players[1].MatchesWon > max)
                        {
                            max = match.Players[1].MatchesWon;
                            TournamentWinner = match.Players[1];
                        }
                    }
                    Console.Clear();
                    Console.WriteLine($"Congrats {TournamentWinner.Name}, you won it all with amazing {TournamentWinner.MatchesWon} points!");
                    Console.ReadLine();
                    // Clear game
                }
                if (UserInput == (int)MainMenuSelection.QuickMatch + 1)
                {
                    List<Player> players = new List<Player>() { new Player { Name = "Human" } };
                    defineHand(players);
                    Random newRandom = new Random();                    
                    players.Add(new Player { Name = "Computer", Hand = WeaponCollection.Weapons[newRandom.Next(0, WeaponCollection.Weapons.Count-1)] });                    
                    compareHands(players);
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

        public static List<Player> definePlayers(int amountPlayers)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < amountPlayers; i++)
            {
                Console.Clear();
                string name;
                Console.WriteLine($"Player {i + 1}, enter your name");
                name = Console.ReadLine();
                players.Add(new Player { Name = name , id = i});
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

                // Compare the keys... make a static userInput helper, perhaps with a few variations (different overloads)
                string userSelection = null;
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                    {   
                        Console.Write("*");
                        userSelection += key.KeyChar;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(userSelection))
                        {
                            // remove one character from the list of password characters
                            userSelection = userSelection.Substring(0, userSelection.Length - 1);
                            // get the location of the cursor
                            int pos = Console.CursorLeft;
                            // move the cursor to the left by one character
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            // replace it with space
                            Console.Write(" ");
                            // move the cursor to the left by one character again
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    else
                    {
                        try
                        {
                            p.Hand = WeaponCollection.Weapons[int.Parse(userSelection) - 1];
                            break;
                        }
                        catch
                        {
                            
                            Console.WriteLine("\nMust type a valid number from 1 to " + WeaponCollection.Weapons.Count);
                            userSelection = "";
                        }                       
                   
                    }
                 
                }               
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
