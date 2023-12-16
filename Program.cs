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

            ElementalChance();
            //MakeOnePick();
        }

        //0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29
        //10, 13, 25, 7, 29, 15, 11, 12, 23, 21, 27, 9, 2, 6, 8, 3, 16, 26, 1, 18, 4, 19, 0, 28, 24, 14, 20, 17, 22, 5
        //0, 13, 15, 7, 29, 25, 11, 12, 23, 21, 27, 9, 2, 6, 8, 3, 16, 26, 1, 18, 4, 19, 10, 28, 24, 14, 20, 17, 22, 5

        private static bool BadConditionIsMet(List<int> cardsInHand)
        {
            return true;
            //return card != 0;
            //return cardsInHand.Any(x => new[] { 0, 1 }.Contains(x));
        }
        private static bool GoodConditionIsMet(List<int> cardsInHand)
        {
            //return true;
            //return card != 0;
            return cardsInHand.Any(x => new[] { 6 }.Contains(x));
        }

        private static bool MulliganConditions(int card)
        {
            return new int[]{ 0, 1, 2, 3, 4, 5 }.Contains(card);
            //return card % 15 == 0;
        }



        public static void ElementalChance()
        {
            int result = 0;
            int iterations = 1000;

            for (int k = 0; k < iterations; k++)
            {
                if(MakeOnePick())
                    result++;
            }

            Console.WriteLine("Result: " + result);

        }

        private static bool MakeOnePick()
        {
            int numberDraws = 7;
            int sizeOfDeck = 30;
            int cardsInMulligan = 3;

            var sortedCards = Extensions.FillArray(sizeOfDeck);
            var shuffleCards = _rng.Shuffle(sortedCards);

            /*while (!(shuffleCards[2] == 15))
            {
                shuffleCards = _rng.Shuffle(sortedCards);
            }*/
            List<int> cardsInHand = MakeMulligan(cardsInMulligan, ref shuffleCards);

            /*Console.WriteLine(shuffleCards.ToStringPretty());
            Console.WriteLine("After reshuffling:");
            Console.WriteLine(shuffleCards.ToStringPretty());*/

            int i = 0;
            while (cardsInHand.Count < numberDraws)
            {
                int drawedCard = shuffleCards[i];

                cardsInHand.Add(drawedCard);
                i++;
            }

            if (BadConditionIsMet(cardsInHand) && GoodConditionIsMet(cardsInHand))
            {
                Console.WriteLine(cardsInHand.ToArray().ToStringPretty());
                return true;
            }

            /*Console.WriteLine("Cards in hands:");
            Console.WriteLine(cardsInHand.ToArray().ToStringPretty());

            Console.WriteLine("Bad condition:" + badCondition);*/
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
                /*Console.WriteLine($"Before reshuffling({secondShuffle.Length}):");
                Console.WriteLine(secondShuffle.ToStringPretty());*/
                shuffleCards = _rng.Shuffle(secondShuffle);
            }

            return cardsInHand;
        }
    }
}
