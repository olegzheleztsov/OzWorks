using System.Diagnostics;
using Oz.Algorithms.Rod;

namespace Oz.Rob
{
    using static System.Console;
    using static System.String;
    
    public class RecursiveSolutions
    {
        public void PrintFactorials(int maxNumber)
        {
            for (int i = 0; i <= maxNumber; i++)
            {
                WriteLine($"{i}! = {i.Factorial()}");
            }
        }

        public void PrintFibonacci(int number)
        {
            var stopwatch = new Stopwatch();
            for (int i = 0; i <= number; i++)
            {
                stopwatch.Restart();
                var fibNumber = i.Fibonacci();
                stopwatch.Stop();
                WriteLine($"Fibonacci({i}) = {fibNumber}, time: {stopwatch.Elapsed.TotalSeconds} secs");
            }
        }

        public void PrintHanoi(int discCount)
        {
            for (var i = 1; i <= discCount; i++)
            {
                WriteLine($"Disc count: {i}");
                var hanoi = new HanoiSolver(i);
                hanoi.Run();
                WriteLine();
                WriteLine();
            }
        }
    }
}