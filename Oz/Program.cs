#region

using Oz.Algorithms.DataStructures;
using Oz.LeetCode.TopQuestions;
using Oz.Rob;
using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
#endregion

namespace Oz
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var solutions = new TopQuestionSolutions();
            WriteLine(solutions.Multiply("2", "3"));
            WriteLine(solutions.Multiply("123", "456"));
        }

        private static void TimerCallback(object o)
        {
            Console.WriteLine($"In TimerCallback: {DateTime.Now}");
            GC.Collect();
        }

        private static void MaxPriorityQueueSample()
        {
            var queue = new MaxPriorityQueue<int>();
            queue.Insert(1, 1);
            queue.Insert(2, 2);
            queue.Insert(3, 3);

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