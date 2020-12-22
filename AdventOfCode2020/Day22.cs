using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day22
    {
        public static long One()
        {
            var input = InputParser.GetInputText("day22.txt").Split(Environment.NewLine + Environment.NewLine);
            var player1Stack = input[0].Split(Environment.NewLine).ToList();
            player1Stack = player1Stack.Skip(1).Take(player1Stack.Count() - 1).ToList();
            var player2Stack = input[1].Split(Environment.NewLine).ToList();
            player2Stack = player2Stack.Skip(1).Take(player2Stack.Count() - 1).ToList();

            while (player1Stack.Count != 0 && player2Stack.Count != 0)
            {
                PlayRound(player1Stack, player2Stack);
            }

            var winner = player1Stack.Count == 0 ? player2Stack : player1Stack;
            return CalculateScore(winner);
        }

        private static long CalculateScore(List<string> winner)
        {
            long total = 0;
            for (int i = 1 ; i <= winner.Count; i++)
            {
                total += Convert.ToInt64(i) * long.Parse(winner[winner.Count - i]);
            }
            return total;
        }

        private static void PlayRound(List<string> player1Stack, List<string> player2Stack)
        {
            var player1Card = player1Stack[0];
            var player2Card = player2Stack[0];
            player1Stack.RemoveAt(0);
            player2Stack.RemoveAt(0);

            if (int.Parse(player1Card) > int.Parse(player2Card))
            {
                player1Stack.Add(player1Card);
                player1Stack.Add(player2Card);
            }
            else
            {
                player2Stack.Add(player2Card);
                player2Stack.Add(player1Card);
            }
        }

        public static long Two()
        {
            var input = InputParser.GetInputText("day22.txt").Split(Environment.NewLine + Environment.NewLine);
            var player1Stack = input[0].Split(Environment.NewLine).ToList();
            player1Stack = player1Stack.Skip(1).Take(player1Stack.Count() - 1).ToList();
            var player2Stack = input[1].Split(Environment.NewLine).ToList();
            player2Stack = player2Stack.Skip(1).Take(player2Stack.Count() - 1).ToList();

            PlayRecursiveGame(player1Stack, player2Stack);

            var winner = player1Stack.Count == 0 ? player2Stack : player1Stack;
            return CalculateScore(winner);
        }

        private static int PlayRecursiveGame(List<string> player1Stack, List<string> player2Stack)
        {
            HashSet<string> previousPlayer1Decks = new HashSet<string>();
            HashSet<string> previousPlayer2Decks = new HashSet<string>();
            while (player1Stack.Count != 0 && player2Stack.Count != 0)
            {
                if (previousPlayer1Decks.Contains(string.Join(",", player1Stack)))
                {
                    return 1;
                }
                if (previousPlayer2Decks.Contains(string.Join(",", player2Stack)))
                {
                    return 1;
                }
                previousPlayer1Decks.Add(string.Join(",", player1Stack));
                previousPlayer2Decks.Add(string.Join(",", player2Stack));

                var player1Card = player1Stack[0];
                var player2Card = player2Stack[0];

                if (player1Stack.Count - 1 >= int.Parse(player1Card) && player2Stack.Count - 1 >= int.Parse(player2Card))
                {
                    player1Stack.RemoveAt(0);
                    player2Stack.RemoveAt(0);

                    var player1SubStack = player1Stack.Take(int.Parse(player1Card)).ToList();
                    var player2SubStack = player2Stack.Take(int.Parse(player2Card)).ToList();
                    var result = PlayRecursiveGame(player1SubStack, player2SubStack);
                    if (result == 1)
                    {
                        player1Stack.Add(player1Card);
                        player1Stack.Add(player2Card);
                    }
                    else
                    {
                        player2Stack.Add(player2Card);
                        player2Stack.Add(player1Card);
                    }
                }
                else
                {
                    PlayRound(player1Stack, player2Stack);
                }
            }
            return player1Stack.Count == 0 ? 2 : 1;
        }
    }
}
