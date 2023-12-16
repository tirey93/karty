using System;
using System.Collections.Generic;
using System.Text;

namespace karty
{
    static class Extensions
    {
        public static T[] Shuffle<T>(this Random rng, T[] array)
        {
            var newArray = (T[]) array.Clone();
            int n = newArray.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = newArray[n];
                newArray[n] = newArray[k];
                newArray[k] = temp;
            }
            return newArray;
        }

        public static string ToStringPretty(this int[] array)
        {
            return string.Join(", ", array);
        }

        public static int[] FillArray(int items)
        {
            var result = new int[items];
            for (int i = 0; i < items; i++)
            {
                result[i] = i;
            }
            return result;
        }

        
    }
}
