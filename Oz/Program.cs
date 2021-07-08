﻿#region
using System;
using System.Threading;
using System.Threading.Tasks;
using Oz.Algorithms.DataStructures;
using Oz.LeetCode.Easy;
using Oz.LeetCode.Recursion;

#endregion

namespace Oz
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] prices = {7, 1, 5, 3, 6, 4};
            var solution = new EasyTasks();
            Console.WriteLine(solution.MaxProfit(prices));

            prices = new[] {1, 2, 3, 4, 5};
            Console.WriteLine(solution.MaxProfit(prices));

            prices = new[] {7, 6, 4, 3, 1};
            Console.WriteLine(solution.MaxProfit(prices));
        }

        private static void MaxPriorityQueueSample()
        {
            var queue = new MaxPriorityQueue<int>();
            queue.Insert(1,1);
            queue.Insert(2,2);
            queue.Insert(3,3);
            
            Console.WriteLine(queue.Maximum());
            Console.WriteLine("-------------");
            
            while (queue.Length > 0)
            {
                Console.WriteLine($"Extract max: {queue.ExtractMaximum()}");
            }
        }

        private static void SpinWaitSample()
        {
            var someBoolean = false;
            var numYields = 0;

            var t1 = Task.Factory.StartNew(() =>
            {
                var sw = new SpinWait();
                while (!someBoolean)
                {
                    if (sw.NextSpinWillYield)
                    {
                        numYields++;
                    }

                    sw.SpinOnce();
                }

                Console.WriteLine($"SpinWait called {sw.Count} times, yielded {numYields} times");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                someBoolean = true;
            });
            Task.WaitAll(t1, t2);
        }
    }
}