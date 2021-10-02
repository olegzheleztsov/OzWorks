// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Console;

namespace clscs
{
    public class SyncCompare
    {
        public static void Test()
        {
            var x = 0;
            const int iterations = 10_000_000;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                x++;
            }

            WriteLine($"Incrementing x: {sw.ElapsedMilliseconds:N0}");

            sw.Restart();
            for (var i = 0; i < iterations; i++)
            {
                M();
                x++;
                M();
            }

            WriteLine($"Incrementing x in M: {sw.ElapsedMilliseconds:N0}");

            var sl = new SpinLock(false);
            sw.Restart();
            for (var i = 0; i < iterations; i++)
            {
                var taken = false;
                sl.Enter(ref taken);
                x++;
                sl.Exit();
            }

            WriteLine($"Incrementing x in SpinLock: {sw.ElapsedMilliseconds:N0}");

            using (var swl = new SimpleWaitLock())
            {
                sw.Restart();
                for (var i = 0; i < iterations; i++)
                {
                    swl.Enter();
                    x++;
                    swl.Leave();
                }

                WriteLine($"Incrementing x in SimpleWaitLock: {sw.ElapsedMilliseconds:N0}");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void M()
        {
        }
    }
}