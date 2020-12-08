﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Sort;

namespace Oz.Sort
{
    public class RadixSorterCase
    {
        public void RunStringSorting()
        {
            var array = GenerateStringArray();
            var keySelector = GenerateStringKeySelector(3);
            var sorter = new RadixSorter<string>(keySelector, Comparisions.StandardComparision);
            sorter.Sort(array, 3);
            Console.WriteLine(JsonConvert.SerializeObject(array));
        }

        private string[] GenerateStringArray()
        {
            string[] array =
            {
                "COW", "DOG", "SEA", "RUG", "ROW", "MOB", "BOX", "TAB", "BAR",
                "EAR", "TAR", "DIG", "BIG", "TEA", "NOW", "FOX"
            };

            var keySelector = GenerateStringKeySelector(3);
            var max = 0;
            foreach (var s in array)
            {
                max = s.Select((t, i) => keySelector(s, i)).Prepend(max).Max();
            }

            var newArray = new string[max];
            if (array.Length < max)
            {
                Array.Copy(array, newArray, array.Length);
                for (var i = array.Length; i < newArray.Length; i++)
                {
                    newArray[i] = "AAA";
                }
            }

            return array.Length < max ? newArray : array;
        }

        private Func<string, int, int> GenerateStringKeySelector(int count)
        {
            return (data, index) => (int) data[index] - (int) 'A';
        }

        public void Run()
        {
            var radixSorter = new RadixSorter<Data>(CreateKeySelector(3), Comparisions.StandardComparision);
            var data = GenerateArrayToSort();

            radixSorter.Sort(data, 3);
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        private Data[] GenerateArrayToSort()
        {
            Data[] array =
            {
                new Data(0),
                new Data(0),
                new Data(0),
                new Data(329),
                new Data(457),
                new Data(657),
                new Data(839),
                new Data(436),
                new Data(720),
                new Data(355),
            };
            return array;
        }

        private Func<Data, int, int> CreateKeySelector(int digitCount)
        {
            return (data, index) => data.Value / (int) Math.Pow(10, index) % 10;
        }

        public class Data
        {
            public Data(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}