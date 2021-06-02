using System;
using System.Collections.Generic;
using System.Text;

namespace Oz.Algorithms
{
    public static class Array2DExtensivons
    {
        public static string Str<T>(this T[,] array)
        {
            if (array == null)
            {
                return string.Empty;
            }
            var lower1 = array.GetLowerBound(0);
            var lower2 = array.GetLowerBound(1);
            var upper1 = array.GetUpperBound(0);
            var upper2 = array.GetUpperBound(1);
            var stringBuilder = new StringBuilder();
            
            for (var i = lower1; i <= upper1; i++)
            {
                for (var j = lower2; j <= upper2; j++)
                {
                    stringBuilder.Append($"{array[i, j],-10}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
        
        public static string Str(this bool[,] array)
        {
            if (array == null)
            {
                return string.Empty;
            }
            var lower1 = array.GetLowerBound(0);
            var lower2 = array.GetLowerBound(1);
            var upper1 = array.GetUpperBound(0);
            var upper2 = array.GetUpperBound(1);
            var stringBuilder = new StringBuilder();
            
            for (var i = lower1; i <= upper1; i++)
            {
                for (var j = lower2; j <= upper2; j++)
                {
                    stringBuilder.Append($"{(array[i, j] ? 1 : 0),-10}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string Str<T>(this T[][] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            if (array.Length == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            foreach (var row in array)
            {
                foreach (var element in row)
                {
                    stringBuilder.Append($"{element,-10}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
        
        public static void SetElementsToValue<T>(this T[,] array, T value)
        {
            if (array == null)
            {
                throw new ArgumentException("Array can't be null");
            }
            
            var lower1 = array.GetLowerBound(0);
            var lower2 = array.GetLowerBound(1);
            var upper1 = array.GetUpperBound(0);
            var upper2 = array.GetUpperBound(1);

            for (var i = lower1; i <= upper1; i++)
            {
                for (var j = lower2; j <= upper2; j++)
                {
                    array[i, j] = value;
                }
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
        {
            if (array == null)
            {
                throw new ArgumentException("Array can't be null");
            }
            
            var lower1 = array.GetLowerBound(0);
            var lower2 = array.GetLowerBound(1);
            var upper1 = array.GetUpperBound(0);
            var upper2 = array.GetUpperBound(1);

            for (var i = lower1; i <= upper1; i++)
            {
                for (var j = lower2; j <= upper2; j++)
                {
                    yield return array[i, j];
                }
            }
        }
    }
}