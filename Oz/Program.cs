using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oz.Algorithms.Arrays;
using Oz.Algorithms.Matrices;
using Oz.Algorithms.Search;
using Oz.Algorithms.Sort;

namespace Oz
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var commandExecutor = new CommandExecutor();
            while (true)
            {
                Console.Write("> :");
                var commandString = Console.ReadLine();
                var commandArgs =
                    commandString.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                if (await commandExecutor.RunAsync(commandArgs).ConfigureAwait(false))
                {
                    break;
                }
            }
        }


        private static void MatrixMultiplication()
        {
            var m1 = new FloatMatrix(4, 4, new float[] {1, 2, 3, 4, -8, 3, -2, 1, 5, 5, -6, 3, 6, 5, 2, 1});
            var m2 = new FloatMatrix(4, 4, new float[] {2, 1, 1, 2, 4, -3, -2, -1, 2, -1, 5, 5, 1, 1, 1, 1});
            var classicResult = m1.Multiply(m2);
            Console.WriteLine("Classic =>");
            Console.WriteLine(classicResult);
            var recursiveResult = m1.Multiply(m2);
            Console.WriteLine("recursive =>");
            Console.WriteLine(recursiveResult);

            var fastResult = m1.FastMultiply(m2);
            Console.WriteLine("fast =>");
            Console.WriteLine(fastResult);
        }

        private static void MaxSubArrayTest()
        {
            int[] arr =
            {
                13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7
            };
            var maxSubArray = new MaxSubArray(arr);
            var result = maxSubArray.Value;
            Console.WriteLine(JsonConvert.SerializeObject(result));

            int[] arr2 =
            {
                -13, -3, -25, -20, -3, -16, -23, -18, -20, -7, -12, -5, -22, -15, -4, -7
            };
            var maxSubArray2 = new MaxSubArray(arr2);
            Console.WriteLine(JsonConvert.SerializeObject(maxSubArray2.Value));

            var bruteForcedMaxSubArray = new BruteForcedMaxSubArray(arr);
            var bruteForcedResult = bruteForcedMaxSubArray.Value;
            Console.WriteLine(JsonConvert.SerializeObject(bruteForcedResult));

            var linearMaxSubArray = new LinearMaxSubArray(arr);
            var linearResult = linearMaxSubArray.Value;
            Console.WriteLine(JsonConvert.SerializeObject(linearResult));
        }

        private static void TestIntSorting()
        {
            int[] array =
            {
                5, 2, 4, 6, 2, 1, 3
            };
            Console.WriteLine("Before:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            var sorter = new IntInsertionSorter();
            sorter.Sort(array);
            Console.WriteLine("After ascending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
            sorter.Sort(array, SortDirection.Descending);

            sorter.Sort(array, SortDirection.Descending);
            Console.WriteLine("After descending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            int[] arr2 =
            {
                1
            };
            sorter.Sort(arr2);
            Console.WriteLine("After element array:");
            Console.WriteLine(JsonConvert.SerializeObject(arr2));

            var index3 = new IntLinearSearcher().FindIndex(array, 3);
            Console.WriteLine(index3);

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            var selectionSorter = new SelectionSorter<int>();
            selectionSorter.Sort(array, element => element);
            Console.WriteLine("After selection ascending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            selectionSorter.Sort(array, element => element, SortDirection.Descending);
            Console.WriteLine("After selection descending:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            var mergeSorter = new MergeSorter<int>();
            mergeSorter.Sort(array, element => element);
            Console.WriteLine("After merge sorting descendant:");
            Console.WriteLine(JsonConvert.SerializeObject(array));

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            mergeSorter.Sort(array, element => element, SortDirection.Descending);
            Console.WriteLine("After merge sorting descendant:");
            Console.WriteLine(JsonConvert.SerializeObject(array));
        }
    }
}