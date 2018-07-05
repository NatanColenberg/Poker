using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackOfCards
{
    public static class Global
    {
        public static readonly int NumberOfCardsInPack = 52;
        public static readonly int NumberOfSuits = 4;
		public static readonly int NumOfCardsInOneSuit = NumberOfCardsInPack / NumberOfSuits;
	}
}
