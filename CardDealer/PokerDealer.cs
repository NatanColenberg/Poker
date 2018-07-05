using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using PackOfCards;
using CardPlayer;

namespace CardDealer
{
    public class PokerDealer : Dealer
    {
        public List<Card> flop = null;
        public Card turn = null;
        public Card river = null;
        public List<Card> cardsOnTable {
            get
            {
                List<Card> result = new List<Card>(5);
                if (flop != null)
                    result.AddRange(flop);
                if (turn != null)
                    result.Add(turn);
                if (river != null)
                    result.Add(river);

                return result;
            }
        }

        public PokerDealer(Pack pack, List<Player> players = null) : base(pack, players) { }

        public List<Card> DealFlop()
        {
            try
            {
                flop = pack.GetRandomCards(3);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to deal flop cards", ex);
            }
            return flop;
        }
        public Card DealTurn()
        {
            try
            {
                turn = pack.GetRandomCard();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to deal turn card", ex);
            }
            return turn;
        }
        public Card DealRiver()
        {
            try
            {
                river = pack.GetRandomCard();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to deal river card", ex);
            }
            return river;
        }
         public void ClearCardsFromTable()
        {
            flop = null;
            turn = null;
            river = null;
        }

    }
}
