using System;
using System.Linq;
using System.Threading.Tasks;
using Oz.Algorithms;
using Oz.Algorithms.Arrays;
using Oz.Algorithms.Matrices;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Search;
using Oz.Algorithms.Sort;
using static System.Console;
using static Newtonsoft.Json.JsonConvert;

namespace Oz
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            RandomSampleCase();
        }

        private static void RandomSampleCase()
        {
            for (int i = 0; i < 20; i++)
            {
                var randomSample = new RandomSample(5, 10);
                WriteLine(SerializeObject(randomSample.Sample));
            }
        }

        private static void RandomizedInterviewCase()
        {
            for (var i = 0; i < 20; i++)
            {
                var candidates = Enumerable.Range(1, 10).ToArray();
                var interview = new RandomizedInterview<int>(candidates, c => c);
                var (bestCandidate, bestIndex, hireCount) = interview.BestCandidate;
                WriteLine(
                    $"Best candidate: {bestCandidate}, best index: {bestIndex}, hire count: {hireCount}, analytical hire count: {Math.Log2(candidates.Length)}");
            }
        }

        private static void ShuffleInPlaceCase()
        {
            for (var i = 0; i < 20; i++)
            {
                var array = Enumerable.Range(1, 10).ToArray();
                array.ShuffleInPlace();
                WriteLine(SerializeObject(array));
            }
        }

        private static void InterviewTest()
        {
            var arr = Enumerable.Range(0, 1000).ToArray();
            var interview = new Interview<int>(new ShuffledArray<int>(arr, new DefaultRandomSource()),
                candidate => candidate);
            var (candidate, index, hiredCount) = interview.BestCandidate;
            WriteLine(
                $"Candidate: {candidate}, index: {index}, hiredCount: {hiredCount}, log2(10000): {Math.Log2(arr.Length)}");
        }

        private static void MatrixMultiplication()
        {
            var m1 = new FloatMatrix(4, 4, new float[] {1, 2, 3, 4, -8, 3, -2, 1, 5, 5, -6, 3, 6, 5, 2, 1});
            var m2 = new FloatMatrix(4, 4, new float[] {2, 1, 1, 2, 4, -3, -2, -1, 2, -1, 5, 5, 1, 1, 1, 1});
            var classicResult = m1.Multiply(m2);
            WriteLine("Classic =>");
            WriteLine(classicResult);
            var recursiveResult = m1.Multiply(m2);
            WriteLine("recursive =>");
            WriteLine(recursiveResult);

            var fastResult = m1.FastMultiply(m2);
            WriteLine("fast =>");
            WriteLine(fastResult);
        }

        private static void MaxSubArrayTest()
        {
            int[] arr =
            {
                13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7
            };
            var maxSubArray = new MaxSubArray(arr);
            var result = maxSubArray.Value;
            WriteLine(SerializeObject(result));

            int[] arr2 =
            {
                -13, -3, -25, -20, -3, -16, -23, -18, -20, -7, -12, -5, -22, -15, -4, -7
            };
            var maxSubArray2 = new MaxSubArray(arr2);
            WriteLine(SerializeObject(maxSubArray2.Value));

            var bruteForcedMaxSubArray = new BruteForcedMaxSubArray(arr);
            var bruteForcedResult = bruteForcedMaxSubArray.Value;
            WriteLine(SerializeObject(bruteForcedResult));

            var linearMaxSubArray = new LinearMaxSubArray(arr);
            var linearResult = linearMaxSubArray.Value;
            WriteLine(SerializeObject(linearResult));
        }

        private static void TestIntSorting()
        {
            int[] array =
            {
                5, 2, 4, 6, 2, 1, 3
            };
            WriteLine("Before:");
            WriteLine(SerializeObject(array));
            var sorter = new IntInsertionSorter();
            sorter.Sort(array);
            WriteLine("After ascending:");
            WriteLine(SerializeObject(array));
            sorter.Sort(array, SortDirection.Descending);

            sorter.Sort(array, SortDirection.Descending);
            WriteLine("After descending:");
            WriteLine(SerializeObject(array));

            int[] arr2 =
            {
                1
            };
            sorter.Sort(arr2);
            WriteLine("After element array:");
            WriteLine(SerializeObject(arr2));

            var index3 = new IntLinearSearcher().FindIndex(array, 3);
            WriteLine(index3);

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            var selectionSorter = new SelectionSorter<int>();
            selectionSorter.Sort(array, element => element);
            WriteLine("After selection ascending:");
            WriteLine(SerializeObject(array));

            selectionSorter.Sort(array, element => element, SortDirection.Descending);
            WriteLine("After selection descending:");
            WriteLine(SerializeObject(array));

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            var mergeSorter = new MergeSorter<int>();
            mergeSorter.Sort(array, element => element);
            WriteLine("After merge sorting descendant:");
            WriteLine(SerializeObject(array));

            array = new[]
            {
                5, 2, 4, 6, 2, 1, 3
            };
            mergeSorter.Sort(array, element => element, SortDirection.Descending);
            WriteLine("After merge sorting descendant:");
            WriteLine(SerializeObject(array));
        }
    }
}