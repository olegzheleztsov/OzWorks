using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.Numerics
{
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Returns new shuffled enumerable from the source enumerable
        /// </summary>
        /// <param name="array">Array that be shuffled</param>
        /// <typeparam name="T">Array type</typeparam>
        /// <returns>New shuffled enumerable</returns>
        public static T[] Shuffled<T>(this T[] array)
        {
            var shuffler = new ShuffledArray<T>(array, new DefaultRandomSource());
            return shuffler;
        }

        /// <summary>
        ///     Shuffle enumerable in place
        /// </summary>
        /// <param name="array">Array to be shuffled</param>
        /// <typeparam name="T">Array type</typeparam>
        public static void Shuffle<T>(this T[] array)
        {
            var shuffledArray = array.Shuffled();
            Array.Copy(shuffledArray, 0, array, 0, array.Length);
        }

        public static void ShuffleInPlace<T>(this T[] array, IRandomSource randomSource = null)
        {
            if (array == null)
            {
                return;
            }

            if (array.Length < 2)
            {
                return;
            }

            randomSource ??= new DefaultRandomSource();

            for (var i = 0; i < array.Length; i++)
            {
                var randomIndex = randomSource.RandomValue(i, array.Length);
                var temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        public static string GetStringRepresentation<T>(this T[] array, Func<T, string> elementStringConverter)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            for (var i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(i != array.Length - 1
                    ? $" {elementStringConverter(array[i])},"
                    : $" {elementStringConverter(array[i])} ");
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public static string GetStringRepresentation<T>(this T[] array)
            => array.GetStringRepresentation(element => element?.ToString() ?? string.Empty);


        public static T[] Extend<T>(this T[] array, int newLength, T defaultValue)
        {
            var newArray = new T[newLength];
            for (var i = 0; i < newArray.Length; i++)
            {
                if (i < array.Length)
                {
                    newArray[i] = array[i];
                }
                else
                {
                    newArray[i] = defaultValue;
                }
            }

            return newArray;
        }

        public static T[] GetEvenIndices<T>(this T[] array)
        {
            return array.Where((a, i) => i % 2 == 0).ToArray();
        }

        public static T[] GetOddIndices<T>(this T[] array)
        {
            return array.Where((a, i) => i % 2 != 0).ToArray();
        }

        public static double Average(this double[] array)
        {
            if (array == null)
            {
                return 0.0;
            }

            if (array.Length == 0)
            {
                return 0.0;
            }

            var length = array.Length;
            var sum = array.Sum();
            return sum / length;
        }

        public static float Average(this float[] array)
        {
            if (array == null)
            {
                return 0.0F;
            }

            if (array.Length == 0)
            {
                return 0.0F;
            }

            var length = array.Length;
            var sum = array.Sum();
            return sum / length;
        }

        public static double Average(this int[] array)
        {
            if (array == null)
            {
                return 0.0;
            }

            if (array.Length == 0)
            {
                return 0.0;
            }

            var length = array.Length;
            var sum = array.Sum();
            return (double)sum / length;
        }

        public static double StdVariance(this double[] array)
        {
            if (array == null || array.Length == 0)
            {
                return 0.0;
            }

            var avg = array.Average();
            var sum = array.Sum(element => (element - avg) * (element - avg));
            return sum / array.Length;
        }

        public static float StdVariance(this float[] array)
        {
            if (array == null || array.Length == 0)
            {
                return 0.0F;
            }

            var avg = array.Average();
            var sum = array.Sum(element => (element - avg) * (element - avg));
            return sum / array.Length;
        }

        public static double StdVariance(this int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return 0.0F;
            }

            var avg = array.Average();
            var sum = array.Sum(element => (element - avg) * (element - avg));
            return sum / array.Length;
        }

        public static double StdDev(this double[] array)
        {
            var stdVar = array.StdVariance();
            return Util.ApproximatelyZero(stdVar) ? 0.0 : Math.Sqrt(stdVar);
        }
        
        public static double StdDev(this float[] array)
        {
            var stdVar = array.StdVariance();
            return Util.ApproximatelyZero(stdVar) ? 0.0 : Math.Sqrt(stdVar);
        }
        
        public static double StdDev(this int[] array)
        {
            var stdVar = array.StdVariance();
            return Util.ApproximatelyZero(stdVar) ? 0.0 : Math.Sqrt(stdVar);
        }

        public static double Median(this double[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array should not be empty");
            }

            if (array.Length == 1)
            {
                return array[0];
            }
            
            Array.Sort(array);

            var length = array.Length;
            if (length % 2 != 0)
            {
                var middle = length >> 1;
                return array[middle];
            }

            var midNext = length / 2;
            return (array[midNext - 1] + array[midNext]) / 2;
        }
        
        public static float Median(this float[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array should not be empty");
            }

            if (array.Length == 1)
            {
                return array[0];
            }
            
            Array.Sort(array);

            var length = array.Length;
            if (length % 2 != 0)
            {
                var middle = length >> 1;
                return array[middle];
            }

            var midNext = length / 2;
            return (array[midNext - 1] + array[midNext]) / 2;
        }
        
        public static double Median(this int[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array should not be empty");
            }

            if (array.Length == 1)
            {
                return array[0];
            }
            
            Array.Sort(array);

            var length = array.Length;
            if (length % 2 != 0)
            {
                var middle = length >> 1;
                return array[middle];
            }

            var midNext = length / 2;
            return (array[midNext - 1] + array[midNext]) / 2.0;
        }

        /// <summary>
        /// Find most frequent elements in the collection
        /// </summary>
        /// <param name="enumerable">Collection of elements</param>
        /// <typeparam name="T">Type of collection element</typeparam>
        /// <returns>Most frequent elements in collection</returns>
        public static IEnumerable<T> FindModes<T>(this IEnumerable<T> enumerable) where T : notnull
        {
            var counts = new Dictionary<T, int>();

            var bestCount = 0;
            foreach (var item in enumerable)
            {
                if (counts.ContainsKey(item))
                {
                    counts[item]++;
                }
                else
                {
                    counts.Add(item, 1);
                }

                if (counts[item] > bestCount)
                {
                    bestCount = counts[item];
                }
            }

            var result = new List<T>();
            foreach (var (item, count) in counts)
            {
                if (count == bestCount)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}