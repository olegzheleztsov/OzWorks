using System;
using System.Linq;
using Microsoft.VisualBasic;
using Oz.Algorithms.Numerics;

namespace Oz
{
    public class RandomizedSelectCase
    {
        public void Run()
        {
            int[] arr = new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray());
            int fifth = arr.RandomizedSelect(5, i => i);
            int second = arr.RandomizedSelect(2, i => i);
            Console.WriteLine(fifth);
            Console.WriteLine(second);
        }
    }
}