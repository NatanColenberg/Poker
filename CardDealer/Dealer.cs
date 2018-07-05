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
    public class Dealer
    {
        protected Pack pack { get; private set; }
        public List<Player> players;
        public uint pot { get; private set; }

        public Dealer(Pack pack, List<Player> players = null)
        {
            if (pack == null)
                throw new Exception("Dealer can not accept NULL value for pack");

            this.pack = pack;
            this.players = players;
        }

        public void DealCardsToPlayer(Player player, int numberOfCards)
        {
            if (player == null)
                throw new Exception("Can not deal to player with NULL value");

            List<Card> cards = pack.GetRandomCards(numberOfCards);
            player.cards.AddRange(cards);
        }
        public void DealCardsToAllPlayers(int numberOfCards)
        {
            foreach (Player player in players)
            {
                DealCardsToPlayer(player, numberOfCards);
            }
        }

        public void ClearCardsFromAllPlayers()
        {
            foreach (Player player in players)
            {
                player.cards.Clear();
            }
        }

    }
}
