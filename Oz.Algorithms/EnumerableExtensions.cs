using System;
using System.Collections.Generic;
using System.Linq;


namespace Oz.Algorithms
{
    public static class EnumerableExtensions
    {
        public static string GetStringRepresentation<T>(this IEnumerable<T> enumerable, Func<T, string> toString)
        {
            return Numerics.ArrayExtensions.GetStringRepresentation(enumerable.ToArray(), toString);
        }

        public static string GetStringRepresentation<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GetStringRepresentation(element => element?.ToString() ?? string.Empty);
        }
    }
}