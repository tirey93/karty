
using System;
using System.Collections.Generic;
using System.Linq;

namespace karty
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Calculator.SizeOfDeck = 30;
            Calculator.NumberDraws = 9;
            Calculator.CardsInMulligan = 3;

            Calculator.DoFullPicking();
            //MakeOnePick();
        }

        //0, 13, 15, 7, 29, 25, 11, 12, 23, 21, 27, 9, 2, 6, 8, 3, 16, 26, 1, 18, 4, 19, 10, 28, 24, 14, 20, 17, 22, 5

        public static int[] CardsNotContainsAnyOf()
        {
            return new int[] {6, 7 };
        }
        public static int[] CardsContainsOneOf()
        {
            return new[] { 0, 1 };
        }

        public static bool MulliganConditions(int card)
        {
            return new int[]{ 0, 1, 2, 3, 4, 5 }.Contains(card);
        }

    }
}
