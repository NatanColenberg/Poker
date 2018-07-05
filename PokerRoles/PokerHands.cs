using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace PokerRoles
{
    public enum Hand 
    {
        HightCard = 0,
        Pair = 1,
        TwoPair = 2,
        ThreeOfaKind = 3,
        Straight = 4,
        Flush = 5,
        FullHouse = 6,
        FourOfaKind = 7,
        StraightFlush = 8,
        RoyalFlush = 9
    }
    public static class PokerHands
    {
        public static Hand CheckHand(List<Card> playerCards, List<Card> tableCards) 
        {
            InputChecking(playerCards, tableCards);

            List<Card> allCards = new List<Card>();
            allCards.AddRange(playerCards);
            allCards.AddRange(tableCards);

            List<int> allCardNumbers = allCards.Select(card => card.Value).ToList();
            List<Suit> allCardSuits = allCards.Select(card => card.Suit).ToList();

            if (HasRoyalFlush(allCards))
                return Hand.RoyalFlush;
            if (HasStraightFlush(allCards))
                return Hand.StraightFlush;
            if (HasFourOfaKind(allCardNumbers))
                return Hand.FourOfaKind;
            if(HasFullHouse(allCardNumbers))
                return Hand.FullHouse;
            if (HasFlush(allCardSuits))
                return Hand.Flush;
            if (HasStraight(allCardNumbers))
                return Hand.Straight;
            if (HasThreeOfaKind(allCardNumbers))
                return Hand.ThreeOfaKind;
            if (HasTwoPair(allCardNumbers))
                return Hand.TwoPair;
            if (HasPair(allCardNumbers))
                return Hand.Pair;

            else return Hand.HightCard;
        }

        private static bool HasPair(List<int> allCardNumbers) 
        {
            List<int> duplicateKeys = allCardNumbers.GroupBy(cardNumber => cardNumber)
                        .Where(group => group.Count() >= 2)
                        .Select(group => group.Key).ToList();

            if (duplicateKeys.Count >= 1)
                return true;
            
            else return false;
        }

        private static bool HasTwoPair(List<int> allCardNumbers)
        {
            List<int> duplicateKeys = allCardNumbers.GroupBy(cardNumber => cardNumber)
                        .Where(group => group.Count() >= 2)
                        .Select(group => group.Key).ToList();

            if (duplicateKeys.Count >= 2)
                return true;

            else return false;
        }

        private static bool HasThreeOfaKind(List<int> allCardNumbers)
        {
            List<int> duplicateKeys = allCardNumbers.GroupBy(cardNumber => cardNumber)
                        .Where(group => group.Count() >= 3)
                        .Select(group => group.Key).ToList();

            if (duplicateKeys.Count > 0)
                return true;

            else return false;
        }

        private static bool HasStraight(List<int> allCardNumbers)
        {
            int[] cardsInOrder = allCardNumbers.OrderBy(cardNumber => cardNumber).ToArray();

            int straitCount = 0;
            for(int i=0; i < cardsInOrder.Length - 1; i++) 
            {
                // there is no need to check until the end
                if (i > 2 && i - straitCount > 2)
                    return false;

                if (cardsInOrder[i] + 1 == cardsInOrder[i + 1])
                {
                    straitCount++;
                    if (straitCount == 4)
                        return true;
                }

                else
                    straitCount = 0;
            }

            return false;

        }

        private static bool HasFlush(List<Suit> allCardSuits)
        {
            List<Suit> duplicateKeys = allCardSuits.GroupBy(cardSuit => cardSuit)
                        .Where(group => group.Count() >= 5)
                        .Select(group => group.Key).ToList();

            if (duplicateKeys.Count >= 1)
                return true;

            else return false;
        }

        private static bool HasFullHouse(List<int> allCardNumbers)
        {
            List<int> pair = allCardNumbers.GroupBy(cardNumber => cardNumber)
                        .Where(group => group.Count() >= 2)
                        .Select(group => group.Key).ToList();

            List<int> threeOfaKind = allCardNumbers.GroupBy(cardNumber => cardNumber)
            .Where(group => group.Count() >= 3)
            .Select(group => group.Key).ToList();

              bool isOnlyThreeOfaKind = pair.Except(threeOfaKind).Count() == 0;

            if (pair.Count >= 1 && threeOfaKind.Count >= 1 && isOnlyThreeOfaKind == false)
                return true;

            else return false;
        }

        private static bool HasFourOfaKind(List<int> allCardNumbers)
        {
            List<int> duplicateKeys = allCardNumbers.GroupBy(cardNumber => cardNumber)
                        .Where(group => group.Count() >= 4)
                        .Select(group => group.Key).ToList();

            if (duplicateKeys.Count > 0)
                return true;

            else return false;
        }
        private static bool HasStraightFlush(List<Card> cards)
        {
            List<Suit> allCardSuits = cards.Select(card => card.Suit).ToList();
            List<Suit> suits = allCardSuits.GroupBy(cardSuit => cardSuit)
                            .Where(group => group.Count() >= 5)
                            .Select(group => group.Key).ToList();

            if (suits.Count == 0)
                return false;

            Suit fluseSuit = suits.First();

            Card[] flushCardsInOrder = cards.Where(card => card.Suit == fluseSuit)
                                                .OrderBy(card => card.Value).ToArray();
           
            int straitCount = 0;
            for (int i = 0; i < flushCardsInOrder.Length - 1; i++)
            {
                // there is no need to check until the end
                if (i > 2 && i - straitCount > 2)
                    return false;

                if (flushCardsInOrder[i].Value + 1 == flushCardsInOrder[i + 1].Value 
                        && flushCardsInOrder[i].Suit == flushCardsInOrder[i + 1].Suit)
                {
                    straitCount++;
                    if (straitCount == 4)
                        return true;
                }

                else
                    straitCount = 0;
            }

            return false;
        }
        private static bool HasRoyalFlush(List<Card> cards)
        {
            // if cards do not contain an Ace we do not have a Royal Flush (fail quickly)
            bool hasAce = cards.Where(card => card.Value == 14).Count() > 0;
            if (hasAce == false)
                return false;

            List<Suit> allCardSuits = cards.Select(card => card.Suit).ToList();
            List<Suit> suits = allCardSuits.GroupBy(cardSuit => cardSuit)
                            .Where(group => group.Count() >= 5)
                            .Select(group => group.Key).ToList();

            if (suits.Count == 0)
                return false;

            Suit fluseSuit = suits.First();

            Card[] flushCardsInOrder = cards.Where(card => card.Suit == fluseSuit)
                                                .OrderBy(card => card.Value).ToArray();

            int straitCount = 0;
            for (int i = 0; i < flushCardsInOrder.Length - 1; i++)
            {
                // there is no need to check until the end
                if (i > 2 && i - straitCount > 2)
                    return false;

                if (flushCardsInOrder[i].Value + 1 == flushCardsInOrder[i + 1].Value
                        && flushCardsInOrder[i].Suit == flushCardsInOrder[i + 1].Suit)
                {
                    straitCount++;
                    if (straitCount == 4 && flushCardsInOrder[i + 1].Value == 14)
                        return true;
                }

                else
                    straitCount = 0;
            }

            return false;
        }

        private static void InputChecking(List<Card> playerCards, List<Card> tableCards) 
        {
            if (playerCards == null)
                throw new Exception("playerCards canot be NULL.");

            if (playerCards.Count != 2)
                throw new Exception($"playerCards most contain exactly 2 cards (playerCards.Count = {playerCards.Count})");

            if (tableCards == null)
                throw new Exception("tableCards canot be NULL.");

            if (tableCards.Count != 5)
                throw new Exception($"tableCards most contain exactly 5 cards (tableCards.Count = {tableCards.Count})");
        }

    }
}
