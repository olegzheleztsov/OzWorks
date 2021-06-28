#region

using System;
using System.Linq;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Sort;

#endregion

namespace Oz.Sort
{
    public class RadixSorterCase
    {
        public void RunStringSorting()
        {
            var array = GenerateStringArray();
            var keySelector = GenerateStringKeySelector();
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

            var keySelector = GenerateStringKeySelector();
            var max = 0;
            foreach (var s in array)
            {
                max = s.Select((_, i) => keySelector(s, i)).Prepend(max).Max();
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

        private Func<string, int, int> GenerateStringKeySelector()
        {
            return (data, index) => data[index] - 'A';
        }

        public void Run()
        {
            var radixSorter = new RadixSorter<Data>(CreateKeySelector(), Comparisions.StandardComparision);
            var data = GenerateArrayToSort();

            radixSorter.Sort(data, 3);
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        private static Data[] GenerateArrayToSort()
        {
            Data[] array =
            {
                new(0),
                new(0),
                new(0),
                new(329),
                new(457),
                new(657),
                new(839),
                new(436),
                new(720),
                new(355),
            };
            return array;
        }

        private static Func<Data, int, int> CreateKeySelector()
        {
            return (data, index) => data.Value / (int) Math.Pow(10, index) % 10;
        }

        private class Data
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