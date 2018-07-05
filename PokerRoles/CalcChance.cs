using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using PackOfCards;
using System.Collections;

namespace PokerRoles
{
	public static class CalcChance
	{
		public static Dictionary<Hand, double> Calc(List<Card> playerCards, List<Card> tableCards) 
		{
			Pack pack = new Pack();

			pack.Remove(playerCards);
			pack.Remove(tableCards);

			List<List<Card>> permutations = GetPermutations(pack, 7 - playerCards.Count - tableCards.Count);

			Dictionary<Hand, int> handsScore = new Dictionary<Hand, int>();
			foreach (Hand hand in Enum.GetValues(typeof(Hand)).Cast<Hand>())
			{
				handsScore.Add(hand, 0);
			}

			foreach (List<Card> perm in permutations)
			{
				List<Card> permTableCards = new List<Card>(perm);
				permTableCards.AddRange(tableCards);

			    Hand permHand = PokerHands.CheckHand(playerCards, permTableCards);
				handsScore[permHand]++;
			}

			Dictionary<Hand, double> handsChance = new Dictionary<Hand, double>();
			foreach (Hand hand in Enum.GetValues(typeof(Hand)).Cast<Hand>())
			{
				double per = handsScore[hand] != 0 ? (double)handsScore[hand] / permutations.Count : 0;
				handsChance.Add(hand, per);
			}

			return handsChance;
		}

		public static List<List<Card>> GetPermutations(Pack pack, int numOfCards) 
		{
			List<List<Card>> result = new List<List<Card>>();
			if (numOfCards == 0)
				return result;

			List<Card> cards = pack.GetCopyOfCards();

			int[] packIndex = new int[numOfCards];

			for (int i = 0; i < packIndex.Length; i++)
				packIndex[i] = i;

			
			int inxedOfArr = packIndex.Length - 1;
			while (true)
			{
				List<Card> permutation = new List<Card>();
				for (int i=0; i< numOfCards;i++)
				{
					permutation.Add(pack[packIndex[i]]);
				}
				result.Add(permutation);

				int index = inxedOfArr;
				int endOfPack = pack.Count - 1;
				while (packIndex[index] == endOfPack)
				{
					index--;
					endOfPack--;

					if(index == -1)
						return result;
				}
				packIndex[index]++;
				for(int j = index + 1 ; j <= packIndex.Length - 1 ; j++)
				{
					packIndex[j] = packIndex[j - 1] + 1;
				}
			}
				

		}
	}
}
