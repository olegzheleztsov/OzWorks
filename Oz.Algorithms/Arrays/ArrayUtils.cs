using System;

namespace Oz.Algorithms.Arrays
{
    public static class ArrayUtils
    {
        /// <summary>
        ///     Creates array initialized with one value
        /// </summary>
        /// <param name="size">Size of the array</param>
        /// <param name="elementValue">Initial element value for each element</param>
        /// <typeparam name="T">Array element type</typeparam>
        /// <returns>New array that initialized with value</returns>
        public static T[] ArrayOf<T>(int size, T elementValue)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size should be greater or equal than 1");
            }

            var array = new T[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = elementValue;
            }

            return array;
        }
    }
}