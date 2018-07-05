using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using PackOfCards;
using CardPlayer;
using CardDealer;
using PokerRoles;
using System.Diagnostics;

namespace TestPokerProject
{
    class Program
    {
        static void Main(string[] args)
        {
			Pack pack = new Pack();
			Player p1 = new Player("Natan", 100);
			PokerDealer dealer = new PokerDealer(pack, new List<Player>() { p1 });

			dealer.DealCardsToAllPlayers(2);
			dealer.PrintAllPlayersCards();
			Console.WriteLine();
			
			Console.WriteLine("Flop:");
			dealer.DealFlop();
			dealer.PrintCardsOnTable();
			Console.WriteLine();

			Dictionary<Hand, double> chances = CalcChance.Calc(p1.cards, dealer.cardsOnTable);
			PrintChances(chances);
			Console.WriteLine();
			Console.ReadKey();

			Console.WriteLine("Turn:");
			dealer.DealTurn();
			dealer.PrintCardsOnTable();
			Console.WriteLine();

			chances = CalcChance.Calc(p1.cards, dealer.cardsOnTable);
			PrintChances(chances);
			Console.WriteLine();
			Console.ReadKey();

			Console.WriteLine("River:");
			dealer.DealRiver();
			dealer.PrintCardsOnTable();
			Console.WriteLine();


			PrintHand(p1.cards, dealer.cardsOnTable);
			Console.ReadKey();
		}

        private static void PrintHand(List<Card> playerCards, List<Card> tableCards)
        {
            switch (PokerHands.CheckHand(playerCards, tableCards))
            {
                case Hand.RoyalFlush:
                    Console.WriteLine("Yheeee You have a Royal Flush!!!");
                    break;
                case Hand.StraightFlush:
                    Console.WriteLine("Yheeee You have a Straight Flush!!!");
                    break;
                case Hand.FourOfaKind:
                    Console.WriteLine("Yheeee You have Four Of a Kind!!!");
                    break;
                case Hand.FullHouse:
                    Console.WriteLine("Yheeee You have a Full House!!!");
                    break;
                case Hand.Flush:
                    Console.WriteLine("Yheeee You have a Flush!!!");
                    break;
                case Hand.Straight:
                    Console.WriteLine("Yheeee You have a Straight!!!");
                    break;
                case Hand.ThreeOfaKind:
                    Console.WriteLine("Yheeee You have Three Of a Kind!!!");
                    break;
                case Hand.TwoPair:
                    Console.WriteLine("Yheeee You have TWO PAIR!!!");
                    break;
                case Hand.Pair:
                    Console.WriteLine("Yheeee You have a PAIR!!!");
                    break;
                case Hand.HightCard:
                    Console.WriteLine("You have only hight card :(");
                    break;
            }
        }

		public static void PrintChances(Dictionary<Hand, double> chances)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Chances : ");
			foreach (Hand hand in chances.Keys)
			{
			if(chances[hand] != 0)
				sb.AppendLine($"{hand}\t-\t{String.Format("{0:0.000}", chances[hand] * 100)}%");
			}
			Console.WriteLine(sb.ToString());
		}

        private void PrintGame() 
        {

        }
    }

    public static class ExtentionMethods
    {
        public static void PrintAllPlayersCards(this Dealer dealer)
        {
            foreach (Player player in dealer.players)
            {
                string playerName = player.HasName ? player.Name : string.Empty;
                Console.WriteLine($"Player {playerName}:");
                foreach (Card card in player.cards)
                {
                    Console.WriteLine(card);
                }
            }
        }
        public static void PrintCardsOnTable(this PokerDealer dealer)
        {
            PrintCards(dealer.cardsOnTable);
        }
        private static void PrintCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }
        }
        
    }
}
