using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Rod;
using static System.Console;
using Oz.LeetCode;
using Oz.Rob;
using System.Threading.Tasks;

namespace Oz
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000);
                WriteLine("Hello");
            });
            await task.ConfigureAwait(true);
            WriteLine("Hello");
        }

        private static void TestSorting()
        {
            var list1 = new OzSingleLinkedList<int>();
            var source = new DefaultRandomSource();
            for (int i = 0; i < 10; i++)
            {
                list1.InsertLast(source.RandomValue(1, 101));
            }
            Console.WriteLine(list1);
            list1.InsertionSort(Comparisions.StandardComparision);
            Console.WriteLine($"Insertion sort: {list1}");
            
            list1.Clear();
            for (int i = 0; i < 10; i++)
            {
                list1.InsertLast(source.RandomValue(1, 101));
            }
            Console.WriteLine(list1);
            list1.SelectionSort(Comparisions.StandardComparision);
            Console.WriteLine($"Selection sort: {list1}");
        }

        private static void TestSorted()
        {
            var list = new OzSingleLinkedList<int>();
            list.InsertLastRange(new int[] {1, 2, 3, 4, 5});
            WriteLine($"{list} Sorted?: {list.IsSorted(Comparisions.StandardComparision)}");
            
            list.Clear();
            list.InsertLastRange(new int[]{1, 2, 3, 5, 4});
            WriteLine($"{list} Sorted?: {list.IsSorted(Comparisions.StandardComparision)}");

            var dList = new OzDoubleLinkedList<int>();
            dList.InsertLastRange(new [] {1, 2, 3, 4, 5});
            WriteLine($"dl: {dList} Sorted?: {dList.IsSorted(Comparisions.StandardComparision)}");
            
            dList.Clear();
            dList.InsertLastRange(new[] {1, 2, 3, 5, 4});
            WriteLine($"dl: {dList} Sorted?: {dList.IsSorted(Comparisions.StandardComparision)}");

        }

        private static void TestInsertSorted()
        {
            var list = new OzDoubleLinkedList<int>();
            var randomSource = new DefaultRandomSource();
            for (int i = 0; i < 100; i++)
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
            for (int i = 0; i < 10; i++)
            {
                list1.InsertLast(i);
            }
            WriteLine(list1);

            var list2 = new OzDoubleLinkedList<int>();
            for (int i = 0; i < 10; i++)
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

            for (int i = 0; i < 10; i++)
            {
                list.InsertFirst(randomSource.RandomValue(1, 101));
            }
            
            Console.WriteLine($"Max value: {list.Max().Data}");
            Console.WriteLine(list);
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
                Console.WriteLine(value);
            }

            var circleNode = list.GetStartCircleNode();
            Console.WriteLine(circleNode?.Data);
        }
        
        

        private static void TestFindPrime()
        {
            var prime = Numerics.FindPrime(10, 1000);
            Console.WriteLine(prime);
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