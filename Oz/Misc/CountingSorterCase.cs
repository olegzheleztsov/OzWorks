using System;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Sort;

namespace Oz
{
    public class CountingSorterCase
    {
        public void Run()
        {
            var sorter = new CountingSorter<Data>();
            var array = GenerateData();
            sorter.Sort(array, data => data.Key, Comparisions.StandardComparision);
            Console.WriteLine(JsonConvert.SerializeObject(array));
        }

        private Data[] GenerateData()
        {
            Data[] array =
            {
                new Data {Key = 2, Value = 'a'},
                new Data {Key = 5, Value = 'b'},
                new Data {Key = 3, Value = 'c'},
                new Data {Key = 0, Value = 'd'},
                new Data {Key = 2, Value = 'e'},
                new Data {Key = 3, Value = 'f'},
                new Data {Key = 0, Value = 'g'},
                new Data {Key = 3, Value = 'h'},
            };
            return array;
        }

        private class Data
        {
            public int Key { get; set; }
            public char Value { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}