#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Arrays;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Rod;
using Oz.Algorithms.Rod.Search;
using Oz.Algorithms.Rod.Sorting;
using Oz.Algorithms.Rod.Trees;
using Oz.LeetCode;
using Oz.LeetCode.QueueStacks;
using Oz.LeetCode.Recursion;
using Oz.LeetCode.Stacks;
using Oz.LeetCode.Trees;
using Oz.Memory;
using Oz.Rob;
using static System.Console;
using static System.String;

#endregion

namespace Oz
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            DeleteNodesSample();
        }
        
        

        private static void DeleteNodesSample()
        {
            BinaryNode<int> n60 = new BinaryNode<int>(60);
            BinaryNode<int> n35 = new BinaryNode<int>(35);
            var n76 = new BinaryNode<int>(76);
            var n17 = new BinaryNode<int>(17);
            var n42 = new BinaryNode<int>(42);
            var n68 = new BinaryNode<int>(68);
            var n11 = new BinaryNode<int>(11);
            var n24 = new BinaryNode<int>(24);
            var n63 = new BinaryNode<int>(63);
            var n69 = new BinaryNode<int>(69);
            var n23 = new BinaryNode<int>(23);

            n60.LeftChild = n35;
            n60.RightChild = n76;
            n35.LeftChild = n17;
            n35.RightChild = n42;
            n76.LeftChild = n68;
            n17.LeftChild = n11;
            n17.RightChild = n24;
            n68.LeftChild = n63;
            n68.RightChild = n69;
            n24.LeftChild = n23;

            var new35 = n60.FindSortedTreeNode(17, Comparisions.StandardComparision);
            var deletedNode = n60.DeleteSortedTreeNode(new35, Comparisions.StandardComparision);
            n60.TraverseDepthFirst(n =>
            {
                Console.Write(n.Data + " ");
            });
        }

        private static void ZeroOneMatrixTest()
        {
            var mat = new int[][]
            {
                new[]{1,0,1,1,0,0,1,0,0,1},
                new[]{0,1,1,0,1,0,1,0,1,1},
                new[]{0,0,1,0,1,0,0,1,0,0},
                new[]{1,0,1,0,1,1,1,1,1,1},
                new[]{0,1,0,1,1,0,0,0,0,1},
                new[]{0,0,1,0,1,1,1,0,1,0},
                new[]{0,1,0,1,0,1,0,0,1,1},
                new[]{1,0,0,0,1,1,1,1,0,1},
                new[]{1,1,1,1,1,1,1,0,1,0},
                new[]{1,1,1,1,0,1,0,0,1,1}
            };
            var zeroOneSolution = new ZeroOneMatrixSolution();
            zeroOneSolution.UpdateMatrix(mat);
            WriteLine(mat.Str());
        }

        private static void FloodFillTest()
        {
            var array = new int[][]
            {
                new[] {1, 1, 1},
                new[] {1, 1, 0},
                new[] {1, 0, 1}
            };
            var solution = new FloodFillSolution();
            var result = solution.FloodFill(array, 1, 1, 2);
            WriteLine(result.Str());
        }

        private static void TestBinSearchRecursive()
        {
            int[] arr = {1, 2, 3, 4};
            WriteLine($"index of 2: {arr.BinSearchRecursive(2, Comparisions.StandardComparision)}");
            WriteLine($"index of 4: {arr.BinSearchRecursive(4, Comparisions.StandardComparision)}");
            WriteLine($"index of 10: {arr.BinSearchRecursive(10, Comparisions.StandardComparision)}");
        }

        private static void TestBinSearch()
        {
            int[] arr = {1, 2, 3, 4};
            WriteLine($"index of 2: {arr.BinSearch(2, Comparisions.StandardComparision)}");
            WriteLine($"index of 4: {arr.BinSearch(4, Comparisions.StandardComparision)}");
            WriteLine($"index of 10: {arr.BinSearch(10, Comparisions.StandardComparision)}");
        }

        private static void TestLinkedListSearch()
        {
            var list = new OzSingleLinkedList<int>();
            list.InsertLastRange(new int[]{1, 2, 3});
            WriteLine($"index of 2: {list.FindSortedIndex(2, Comparisions.StandardComparision)}");
            WriteLine($"index of 3: {list.FindSortedIndex(3, Comparisions.StandardComparision)}");
            WriteLine($"index of 4: {list.FindSortedIndex(4, Comparisions.StandardComparision)}");
        }

        private static void TestLinearSearch()
        {
            int[] arr = {33, 4, 6, 7, 10};
            int? indexOf4 = arr.LinearSearch(4, Comparisions.StandardComparision);
            WriteLine($"Found index: {indexOf4}");

            int? indexOf6 = arr.LinearSearchRecursive(6, Comparisions.StandardComparision);
            WriteLine($"Found index: {indexOf6}");

            int? indexOf10 = arr.LinearSearchRecursive(10, Comparisions.StandardComparision);
            WriteLine($"Found index: {indexOf10}");
        }

        static async IAsyncEnumerable<int> RangeAsync(int start, int count,
            [EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(i);
                yield return start + i;
            }
        }

        public static void WorkWithSpans()
        {
            var array = new byte[100];
            var arraySpan = new Span<byte>(array);
            InitializeSpan(arraySpan);
            WriteLine($"The sum is {ComputeSum(arraySpan):N0}");

            var native = Marshal.AllocHGlobal(100);
            Span<byte> nativeSpan;
            unsafe
            {
                nativeSpan = new Span<byte>(native.ToPointer(), 100);
            }
            InitializeSpan(nativeSpan);
            WriteLine($"The sum is {ComputeSum(nativeSpan):N0}");
            
            Marshal.FreeHGlobal(native);

            Span<byte> stackSpan = stackalloc byte[100];
            InitializeSpan(stackSpan);
            WriteLine($"The sum is {ComputeSum(stackSpan):N0}");

            void InitializeSpan(Span<byte> span)
            {
                byte value = 0;
                for (int ctr = 0; ctr < span.Length; ctr++)
                {
                    span[ctr] = value++;
                }
            }

            int ComputeSum(Span<byte> span)
            {
                int sum = 0;
                foreach (var value in span)
                {
                    sum += value;
                }

                return sum;
            }
        }

        private static void TestQuicksortOnStacks()
        {
            var arrayGenerator = new ArrayGenerator(1000000);
            var array = arrayGenerator.Generate(1000000);

            Stopwatch stopwatch = new Stopwatch();
            WriteLine("Start sorting on stacks");
            stopwatch.Start();
            array.QuicksortOnStacks(ArrayElementData.Comparison);
            stopwatch.Stop();
            WriteLine($"End on stacks, elapsed {stopwatch.Elapsed.TotalSeconds} secs");
            WriteLine(Join(", ", array.Take(100)));

            array = arrayGenerator.Generate(1000000);
            WriteLine("Start sorting on queues");
            stopwatch.Restart();
            array.QuicksortOnQueues(ArrayElementData.Comparison);
            stopwatch.Stop();
            WriteLine($"End on queues, elapsed: {stopwatch.Elapsed.TotalSeconds} secs");
            WriteLine(Join(", ", array.Take(100)));

            array = arrayGenerator.Generate(1000000);
            WriteLine("Start sorting in place");
            stopwatch.Restart();
            array.Quicksort(ArrayElementData.Comparison);
            stopwatch.Stop();
            WriteLine($"End in place sorting, elapsed: {stopwatch.Elapsed.TotalSeconds} secs");
            WriteLine(Join(", ", array.Take(100)));

            arrayGenerator = new ArrayGenerator(1000);
            var intArray = arrayGenerator.Generate(1000000).Select(d => d.Value).ToArray();
            WriteLine("Start counting sort");
            int maxVal = intArray.Max();
            stopwatch.Restart();
            intArray.CountingSort(maxVal);
            stopwatch.Stop();
            WriteLine($"End count sort, elapsed: {stopwatch.Elapsed.TotalSeconds} secs");
            WriteLine(Join(", ", intArray.Take(100)));
        }

        private static void TestMergesort()
        {
            int[] arr = { };
            arr.Mergesort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {1};
            arr.Mergesort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {2, 1};
            arr.Mergesort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            var source = new DefaultRandomSource();
            arr = Enumerable.Range(0, 10).Select(v => source.RandomValue(1, 20)).ToArray();
            arr.Mergesort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));
        }

        private static void TestQuicksort()
        {
            int[] arr = { };
            arr.Quicksort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {1};
            arr.Quicksort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {2, 1};
            arr.Quicksort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            var source = new DefaultRandomSource();
            arr = Enumerable.Range(0, 10).Select(v => source.RandomValue(1, 20)).ToArray();
            arr.Quicksort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));
        }

        private static void TestHeapSort()
        {
            int[] arr = { };
            arr.HeapSort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {1};
            arr.HeapSort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            arr = new int[] {2, 1};
            arr.HeapSort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));

            var source = new DefaultRandomSource();
            arr = Enumerable.Range(0, 10).Select(v => source.RandomValue(1, 20)).ToArray();
            arr.HeapSort(Comparisions.StandardComparision);
            WriteLine(Join(", ", arr));
        }

        private static void LowerTriangularMatrixTest()
        {
            var matrix = new LowerTriangularArray<int>(5);

            for (var row = 0; row < 5; row++)
            {
                for (var col = 0; col <= row; col++)
                {
                    matrix[row, col] = row + col;
                }
            }

            WriteLine(matrix.ToString());

            try
            {
                WriteLine(matrix[0, 3]);
            }
            catch (IndexOutOfRangeException exception)
            {
                WriteLine(exception.Message);
            }

            var upperMatrix = new UpperTriangularArray<int>(5);

            for (int row = 0; row < 5; row++)
            {
                for (int col = row; col < 5; col++)
                {
                    upperMatrix[row, col] = row + col;
                }
            }
            
            WriteLine(upperMatrix);

            try
            {
                WriteLine(upperMatrix[3, 0]);
            }
            catch (IndexOutOfRangeException exception)
            {
                WriteLine(exception.Message);
            }

            int counter = 0;
            var luMatrix = new LeftUpperTriangularArray<int>(5);
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < (5 - row); col++)
                {
                    luMatrix[row, col] = row + col;
                }
            }
            WriteLine(luMatrix);
            luMatrix.PrintArray();
            try
            {
                WriteLine(luMatrix[2, 3]);
            }
            catch (IndexOutOfRangeException exception)
            {
                WriteLine(exception.Message);
            }
        }

        private static void TestSorting()
        {
            var list1 = new OzSingleLinkedList<int>();
            var source = new DefaultRandomSource();
            for (var i = 0; i < 10; i++)
            {
                list1.InsertLast(source.RandomValue(1, 101));
            }

            WriteLine(list1);
            list1.InsertionSort(Comparisions.StandardComparision);
            WriteLine($"Insertion sort: {list1}");

            list1.Clear();
            for (var i = 0; i < 10; i++)
            {
                list1.InsertLast(source.RandomValue(1, 101));
            }

            WriteLine(list1);
            list1.SelectionSort(Comparisions.StandardComparision);
            WriteLine($"Selection sort: {list1}");
        }

        private static void TestSorted()
        {
            var list = new OzSingleLinkedList<int>();
            list.InsertLastRange(new[] {1, 2, 3, 4, 5});
            WriteLine($"{list} Sorted?: {list.IsSorted(Comparisions.StandardComparision)}");

            list.Clear();
            list.InsertLastRange(new[] {1, 2, 3, 5, 4});
            WriteLine($"{list} Sorted?: {list.IsSorted(Comparisions.StandardComparision)}");

            var dList = new OzDoubleLinkedList<int>();
            dList.InsertLastRange(new[] {1, 2, 3, 4, 5});
            WriteLine($"dl: {dList} Sorted?: {dList.IsSorted(Comparisions.StandardComparision)}");

            dList.Clear();
            dList.InsertLastRange(new[] {1, 2, 3, 5, 4});
            WriteLine($"dl: {dList} Sorted?: {dList.IsSorted(Comparisions.StandardComparision)}");
        }

        private static void TestInsertSorted()
        {
            var list = new OzDoubleLinkedList<int>();
            var randomSource = new DefaultRandomSource();
            for (var i = 0; i < 100; i++)
            {
                var number = randomSource.RandomValue(1, 101);
                WriteLine($"Will insert: {number}");
                list.InsertSorted(number, (a, b) => a.CompareTo(b));
                WriteLine(list);
            }
        }

        private static void TestInsertFirstLastForDLL()
        {
            var list1 = new OzDoubleLinkedList<int>();
            for (var i = 0; i < 10; i++)
            {
                list1.InsertLast(i);
            }

            WriteLine(list1);

            var list2 = new OzDoubleLinkedList<int>();
            for (var i = 0; i < 10; i++)
            {
                list2.InsertFirst(i);
            }

            WriteLine(list2);
            WriteLine();

            while (!list1.IsEmpty)
            {
                list1.Delete(data => data == list1.TailNode.Data);
                WriteLine(list1);
                WriteLine($"Count: {list1.Count}");
            }

            while (!list2.IsEmpty)
            {
                list2.Delete(data => data == list2.HeadNode.Data);
                WriteLine(list2);
                WriteLine($"Count: {list2.Count}");
            }
        }

        private static void TestMaxOnList()
        {
            var list = new OzSingleLinkedList<int>();
            var randomSource = new DefaultRandomSource();

            for (var i = 0; i < 10; i++)
            {
                list.InsertFirst(randomSource.RandomValue(1, 101));
            }

            WriteLine($"Max value: {list.Max().Data}");
            WriteLine(list);
        }


        private static void TestCircles()
        {
            var list = new OzSingleLinkedList<char>();
            list.InsertLast('A');
            list.InsertLast('B');
            list.InsertLast('C');
            list.InsertLast('D');
            list.InsertLast('E');
            list.InsertLast('F');
            list.InsertLast('G');
            list.InsertLast('H');

            var hNode = list.Search(value => value == 'H');
            var dNode = list.Search(value => value == 'D');
            hNode.Next = dNode;

            foreach (var value in list)
            {
                WriteLine(value);
            }

            var circleNode = list.GetStartCircleNode();
            WriteLine(circleNode?.Data);
        }


        private static void TestFindPrime()
        {
            var prime = Numerics.FindPrime(10, 1000);
            WriteLine(prime);
        }

        private static void TestRandomBigInteger()
        {
            var defaultRandomSource = new DefaultRandomSource();
            var frequencies = new Dictionary<BigInteger, int>();

            for (var i = 0; i < 10000; i++)
            {
                var rndNumber = defaultRandomSource.RandomBigInteger(5, 10);
                if (frequencies.ContainsKey(rndNumber))
                {
                    frequencies[rndNumber]++;
                }
                else
                {
                    frequencies[rndNumber] = 1;
                }
            }

            WriteLine(JsonConvert.SerializeObject(frequencies.OrderBy(pair => pair.Key)));
        }
    }
}