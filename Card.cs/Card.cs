using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
    public class Card
    {
        // the numeric value of the card (for example 12 ['Q'])
        public int Value { get; }

        // the suit of the card (for example 'Hearts')
        public Suit Suit { get; }

        public Card(int value, Suit suit)
        {
            // check that the value of the card is valid
            if (IsCardValueValid(value) == false)
                throw new Exception($"Card value must be between {Global.MinCardValue} and {Global.MaxCardValue}");

            this.Value = value;
            this.Suit = suit;
        }
		public Card(Card card)
		{
			Value = card.Value;
			Suit = card.Suit;
		}

		public Card Copy()
		{
			return new Card(Value, Suit);
		}
		protected bool IsCardValueValid(int cardValue)
		{
			return cardValue >= Global.MinCardValue && cardValue <= Global.MaxCardValue;
		}
		public override bool Equals(object obj)
		{
			Card card = (Card)obj;
			return card.Value == Value && card.Suit == Suit;
		}
		public override string ToString()
        {
            string value;

            // if the value of the card is heier that 10 we would like
            // to display is't caracrer rether than his number value (Example value=12, display='Q')
            if (Global.RoyalSymbol.ContainsKey(Value))
                value = Global.RoyalSymbol[Value].ToString();
            else
                value = Value.ToString();

            return $"{value} {Suit}";
        }
    }

}
