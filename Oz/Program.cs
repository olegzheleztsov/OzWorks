#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Arrays;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Rod;
using Oz.Algorithms.Rod.Sorting;
using Oz.LeetCode;
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
            var solutions = new TreeSolutions();
            solutions.TestSymmetric();
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