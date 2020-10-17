using System;
using Newtonsoft.Json;
using Oz.Algorithms.Arrays;
using Oz.Algorithms.Search;
using Oz.Algorithms.Sort;

namespace Oz
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MaxSubArrayTest();
        }

        private static void MaxSubArrayTest()
        {
            int[] arr = {13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7};
            MaxSubArray maxSubArray = new MaxSubArray(arr);
            var result = maxSubArray.Value;
            Console.WriteLine(JsonConvert.SerializeObject(result));
            
            int[] arr2 = {-13, -3, -25, -20, -3, -16, -23, -18, -20, -7, -12, -5, -22, -15, -4, -7};
            MaxSubArray maxSubArray2 = new MaxSubArray(arr2);
            Console.WriteLine(JsonConvert.SerializeObject(maxSubArray2.Value));
        }

        private static void TestIntSorting()
        {
            int[] array = {5, 2, 4, 6, 2, 1, 3};
            Console.WriteLine("Before:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            var sorter = new IntInsertionSorter();
            sorter.Sort(array, SortDirection.Ascending);
            Console.WriteLine("After ascending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            sorter.Sort(array, SortDirection.Descending);
            
            sorter.Sort(array, SortDirection.Descending);
            Console.WriteLine("After descending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            int[] arr2 = {1};
            sorter.Sort(arr2);
            Console.WriteLine("After element array:");
            Console.WriteLine(JsonConvert.SerializeObject(arr2));
            
            var index3 = new IntLinearSearcher().FindIndex(array, 3);
            Console.WriteLine(index3);
            
            array = new [] {5, 2, 4, 6, 2, 1, 3};
            var selectionSorter = new SelectionSorter<int>();
            selectionSorter.Sort(array, element => element, SortDirection.Ascending);
            Console.WriteLine("After selection ascending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            
            selectionSorter.Sort(array, element => element, SortDirection.Descending);
            Console.WriteLine("After selection descending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            
            array = new [] {5, 2, 4, 6, 2, 1, 3};
            var mergeSorter = new MergeSorter<int>();
            mergeSorter.Sort(array, element => element);
            Console.WriteLine("After merge sorting descendant:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            array = new [] {5, 2, 4, 6, 2, 1, 3};
            mergeSorter.Sort(array, element => element, SortDirection.Descending);
            Console.WriteLine("After merge sorting descendant:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
        }
    }
}