using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public static class Global
    {
        public static readonly int MinCardValue = 2;
        public static readonly int MaxCardValue = 14;

        public static readonly Dictionary<Suit, char> SuitSymbols = 
            new Dictionary<Suit, char>() {
                { Suit.Hearts ,'♥' },
                { Suit.Diamonds, '♦' },
                { Suit.Clubs ,'♠' },
                { Suit.Spades, '♣' }
            };

        public static readonly Dictionary<int, char> RoyalSymbol =
            new Dictionary<int, char>() {
                { 11 ,'J' },
                { 12, 'Q' },
                { 13 ,'K' },
                { 14, 'A' }
            };
    }
}
