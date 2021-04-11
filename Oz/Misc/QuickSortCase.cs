using System;
using System.Linq;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Sort;

namespace Oz
{
    public class QuickSortCase
    {
        public void Run()
        {
            var array = GenerateArrayToSort();
            var sorter = new QuickSorter<Data>();
            sorter.Sort(array, data => data.Key, (a, b) => a.CompareTo(b));
            Console.WriteLine(JsonConvert.SerializeObject(array));
        }

        private Data[] GenerateArrayToSort()
        {
            var random = new DefaultRandomSource();
            return Enumerable.Range(1, 100).Select(element => new Data()
            {
                Key = element,
                Value = random.RandomDouble
            }).ToArray();
        }
        
        public class Data
        {
            public int Key { get; set; }
            public double Value { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}