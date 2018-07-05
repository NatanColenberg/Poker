using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace PackOfCards
{
    public class Pack
    {
		// list of cards that are in the pack
		private List<Card> cards;

		// random numbers generator
		private Random random;

		/// <summary>
		/// Number of cards in the pack
		/// </summary>
		public int Count { get { return cards.Count; } }

		public Pack()
		{
			// Init the pack
			Reset();
		}
		public Pack(List<Card> cards)
		{
			this.cards = cards;
			random = new Random(DateTime.Now.Millisecond);
		}

		// Public Methods

		/// <summary>
		/// Returns True is the card exist in the pack
		/// </summary>
		/// <param name="card"></param>
		/// <returns>True if the card Value and Suit match to at list one of the cards in the pack</returns>
		public bool Exist(Card card)
		{
			return cards.Contains(card);
		}
		/// <summary>
		/// Returns the number of cards with a certin Suit from the pack
		/// </summary>
		/// <param name="suit"></param>
		/// <returns></returns>
		public int CountSuit(Suit suit)
		{
			return cards.Where(card => card.Suit == suit).Count();
		}
		/// <summary>
		/// Returns the number of cards with a certin Value from the pack
		/// </summary>
		/// <param name="cardValue"></param>
		/// <returns></returns>
		public int CountValue(int cardValue)
		{
			return cards.Where(card => card.Value == cardValue).Count();
		}
		/// <summary>
		/// This method pop's a Card out of the Pack
		/// </summary>
		/// <returns></returns>
		public Card GetRandomCard()
		{
			List<Card> randomCards = GetRandomCards(1);

			if (randomCards != null)
				return randomCards.First();

			else return null;
		}
		/// <summary>
		/// This method pop's a number of cards out of the Pack
		/// </summary>
		/// <param name="numberOfcards"></param>
		/// <returns></returns>
		public List<Card> GetRandomCards(int numberOfcards)
		{
			if (Count < numberOfcards)
				return null;

			List<Card> resultcards = new List<Card>(numberOfcards);
			for (int i = 0; i < numberOfcards; i++)
			{
				int cardIndexInPack = random.Next(0, Count - 1 - i);
				Card resultcard = cards[cardIndexInPack];
				resultcards.Add(resultcard);
			}

			Remove(resultcards);
			return resultcards;
		}
		/// <summary>
		/// Reset the Pack by getting new cards and resetting the random numbers generator
		/// </summary>
		public void Reset()
		{
			ResetCards();
			ResetRandomNumberGenerator();
		}
		/// <summary>
		/// Replacing the current cards with a new set of cards
		/// </summary>
		public void ResetCards()
		{
			cards = GetNewCards();
		}
		/// <summary>
		/// Adds a card to the pack
		/// </summary>
		/// <param name="card"></param>
		public void Add(Card card)
		{
			Add(new List<Card>(1) { card });
		}
		/// <summary>
		/// Adds a list of cards to the pack
		/// </summary>
		/// <param name="cardsToAdd"></param>
		public void Add(List<Card> cardsToAdd)
		{
			cards.AddRange(cardsToAdd);
		}
		/// <summary>
		/// Removes a card from the pack
		/// </summary>
		/// <param name="card"></param>
		public void Remove(Card card)
		{
			Remove(new List<Card>(1) { card });
		}
		/// <summary>
		/// Removes a list of cards from the pack
		/// </summary>
		/// <param name="cards"></param>
		public void Remove(List<Card> cards)
		{
			List<Card> cardsToRemove = new List<Card>(cards.Count);
			foreach (Card card in cards)
			{
				Card cardToRemoved = cards.FirstOrDefault(c => c == card);

				if(cardToRemoved == null)
					throw new Exception($"Could not find the card in this pack (card = '{card}')");

				cardsToRemove.Add(cardToRemoved);
			}

			this.cards.RemoveAll(cardInPack => cardsToRemove.Contains(cardInPack));
		}
		/// <summary>
		/// Returns a list of copies of the cards in the pack
		/// </summary>
		/// <returns></returns>
		public List<Card> GetCopyOfCards()
		{
			List<Card> copyCards = new List<Card>();
			foreach (Card card in cards)
			{
				copyCards.Add(card.Copy());
			}
			return copyCards;
		}
		/// <summary>
		/// Returns the card with the index from the pack
		/// </summary>
		/// <param name="index"></param>
		/// <returns>card with the index from the pack or NULL if index does not exist</returns>
		public Card this[int index]
		{
			get 
			{
				if (index > Count - 1 || index < 0)
					return null;

				return cards[index];
			}
		}
		/// <summary>
		/// Returns the card the matches from the pack
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		public Card this[Card card]
		{
			get
			{
				return cards.FirstOrDefault(c => c == card);
			}
		}
		
		// Private Methods

		/// <summary>
		/// Return a new set of cards
		/// </summary>
		/// <returns></returns>
		private List<Card> GetNewCards()
		{
			// Allocate new Pack
			List<Card> newCards = new List<Card>(Global.NumberOfCardsInPack);

			// iterate throgh the Values
			for (int cardValue = Cards.Global.MinCardValue; cardValue <= Cards.Global.MaxCardValue; cardValue++)
			{
				// iterate throgh the Suits
				foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
				{
					// Add a card to the New Pack
					newCards.Add(new Card(cardValue, suit));
				}
			}

			return newCards;
		}
		/// <summary>
		/// This method resets the random number generator
		/// </summary>
		private void ResetRandomNumberGenerator()
		{
			random = new Random(DateTime.Now.Millisecond);
		}
	}
}
