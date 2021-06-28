#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Oz.LeetCode.Recursion;

#endregion

namespace Oz
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GenerateParenthesisSolver.PrintMissed();
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