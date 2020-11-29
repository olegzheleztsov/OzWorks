using System;
using System.Text;

namespace Oz.Algorithms.Numerics
{
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Returns new shuffled array from the source array
        /// </summary>
        /// <param name="array">Array that be shuffled</param>
        /// <typeparam name="T">Array type</typeparam>
        /// <returns>New shuffled array</returns>
        public static T[] Shuffled<T>(this T[] array)
        {
            var shuffler = new ShuffledArray<T>(array, new DefaultRandomSource());
            return shuffler;
        }

        /// <summary>
        ///     Shuffle array in place
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
    }
}