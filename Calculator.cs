using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace karty
{
    internal static class Calculator
    {
        private static Random _rng = new Random();
        public static int NumberDraws {  get; set; }
        public static int SizeOfDeck { get; set; }
        public static int CardsInMulligan { get; set; }

        public static void DoFullPicking()
        {
            int result = 0;
            int all = 0;

#if DEBUG
            int iterations = 100;    
            for (int i = 0; i < iterations; i++)
#else
            while (true)
#endif
            {
                all++;
                if (MakeOnePick())
                    result++;
#if !DEBUG
                if (all % 10000 == 0)
                    Console.Write(FormatResult(result, all));
#endif
            }

            Console.WriteLine("\n\n" + FormatResult(result, all));
        }

        private static string FormatResult(int result, int all)
        {
            return $"\rResult: {(double)result / all * 100:n1}% / {all:n0}";
        }

        private static bool MakeOnePick()
        {
            var sortedCards = Extensions.FillArray(SizeOfDeck);
            var shuffleCards = _rng.Shuffle(sortedCards);

            List<int> cardsInHand = MakeMulligan(CardsInMulligan, ref shuffleCards);

            int i = 0;
            while (cardsInHand.Count < NumberDraws)
            {
                int drawedCard = shuffleCards[i];

                cardsInHand.Add(drawedCard);
                i++;
            }

            ;
            if (!cardsInHand.Any(x => Program.CardsNotContainsAnyOf().Contains(x))
                && cardsInHand.Any(x => Program.CardsContainsOneOf().Contains(x)))
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
                if (Program.MulliganConditions(shuffleCards[i]))
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
