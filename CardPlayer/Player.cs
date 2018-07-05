using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace CardPlayer
{
    public class Player
    {
        private static readonly int DefaultNumberOfCardsAllowd = 2;

        public static int NumberOfCardsInHand { get; set; } = DefaultNumberOfCardsAllowd;

        public List<Card> cards { get; }
        public string Name { get; }
        public bool HasName { get { return (string.IsNullOrEmpty(Name) == false); } }
        public int Equity { get; private set; }

        public Player(string name = null, int equity = 0)
        {
            cards = new List<Card>(NumberOfCardsInHand);
            this.Name = name;
            this.Equity = equity;
        }
    }
}
