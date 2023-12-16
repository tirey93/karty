
using System;
using System.Collections.Generic;
using System.Linq;

namespace karty
{
    internal class Program
    {
        private static Random _rng;

        static void Main(string[] args)
        {
            _rng = new Random();

            DoFullPicking();
            //MakeOnePick();
        }

        //0, 13, 15, 7, 29, 25, 11, 12, 23, 21, 27, 9, 2, 6, 8, 3, 16, 26, 1, 18, 4, 19, 10, 28, 24, 14, 20, 17, 22, 5

        private static int[] CardsNotContainsAnyOf()
        {
            return new int[] {6, 7 };
        }
        private static int[] CardsContainsOneOf()
        {
            return new[] { 0, 1 };
        }

        private static bool MulliganConditions(int card)
        {
            return new int[]{ 0, 1, 2, 3, 4, 5 }.Contains(card);
        }



        public static void DoFullPicking()
        {
            int result = 0;
            int iterations = 100;
            int all = 0;

#if DEBUG
                for (int i = 0; i < iterations; i++)
#else
                while(true)
#endif
            {
                all++;
                if(MakeOnePick())
                    result++;
#if !DEBUG
                if (all % 10000 == 0)
                    Console.Write($"\rResult: {(double)result / all:n3} / {all}");
#endif
            }

            Console.WriteLine($"\n\nResult: {(double)result / all:n3} / {all}");
        }

        private static bool MakeOnePick()
        {
            int numberDraws = 9;
            int sizeOfDeck = 30;
            int cardsInMulligan = 3;

            var sortedCards = Extensions.FillArray(sizeOfDeck);
            var shuffleCards = _rng.Shuffle(sortedCards);

            List<int> cardsInHand = MakeMulligan(cardsInMulligan, ref shuffleCards);

            int i = 0;
            while (cardsInHand.Count < numberDraws)
            {
                int drawedCard = shuffleCards[i];

                cardsInHand.Add(drawedCard);
                i++;
            }

            ;
            if (!cardsInHand.Any(x => CardsNotContainsAnyOf().Contains(x)) 
                && cardsInHand.Any(x => CardsContainsOneOf().Contains(x)))
            {
#if DEBUG
                Console.WriteLine("## Hand: " + cardsInHand.ToArray().ToStringPretty());
                Console.WriteLine();
#endif
                    
                return true;
            }
            return false;
        }

        private static List<int> MakeMulligan(int cardsInMulligan, ref int[] shuffleCards)
        {
            var toBeReshuffle = new Dictionary<int, bool>();
            for (int i = 0; i < cardsInMulligan; i++)
            {
                if (MulliganConditions(shuffleCards[i]))
                {
                    toBeReshuffle.Add(shuffleCards[i], true);
                }
                else
                {
                    toBeReshuffle.Add(shuffleCards[i], false);
                }
            }

            var cardsInHand = new List<int>();
            if (toBeReshuffle.Where(x => x.Value).Count() > 0)
            {
                foreach (var card in toBeReshuffle.Where(x => !x.Value))
                {
                    cardsInHand.Add(card.Key);
                }

                var secondShuffle = shuffleCards.Where(x => !cardsInHand.Contains(x)).ToArray();
                shuffleCards = _rng.Shuffle(secondShuffle);

#if DEBUG
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Mulligan: " + toBeReshuffle.Keys.ToArray().ToStringPretty());
                Console.WriteLine("Second: " + secondShuffle.ToStringPretty());
                Console.WriteLine("Reshuffle: " + shuffleCards.ToStringPretty());
#endif
            }

            return cardsInHand;
        }
    }
}
