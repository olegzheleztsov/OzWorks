#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

namespace Oz.Rob
{
    public class ComputationTable
    {
        private readonly Dictionary<string, int> _funcOrder = new()
        {
            ["log(x)"] = 0,
            ["sqrt(x)"] = 1,
            ["x"] = 2,
            ["x^2"] = 3,
            ["2^x"] = 4,
            ["Finv"] = 5
        };

        private readonly Dictionary<string, Func<double, double>> _invertedFunctions;
        private readonly Dictionary<string, double> _times;

        public ComputationTable()
        {
            const int opPerSecond = 1000000;
            _invertedFunctions = new Dictionary<string, Func<double, double>>
            {
                ["log(x)"] = x => Math.Pow(2.0, x),
                ["sqrt(x)"] = x => x * x,
                ["x"] = x => x,
                ["x^2"] = Math.Sqrt,
                ["2^x"] = Math.Log2,
                ["Finv"] = x => InverseFactorial(x)
            };

            _times = new Dictionary<string, double>
            {
                ["sec"] = opPerSecond,
                ["min"] = 60.0 * opPerSecond,
                ["hour"] = 3600.0 * opPerSecond,
                ["day"] = 86400.0 * opPerSecond,
                ["weak"] = 7.0 * 86400.0 * opPerSecond,
                ["year"] = 365.0 * 7.0 * 86400.0 * opPerSecond
            };
        }

        public void PrintTable()
        {
            foreach (var (key, value) in _times.OrderBy(kvp => kvp.Value))
            {
                Console.Write($"{key}: ");
                foreach (var funcPair in _invertedFunctions.OrderBy(kvp => _funcOrder[kvp.Key]))
                {
                    double result;
                    try
                    {
                        result = funcPair.Value(value);
                    }
                    catch
                    {
                        result = -1;
                    }

                    Console.Write(
                        $"{funcPair.Key}: {(result < 0 ? "INF" : result.ToString("F2"))}\t");
                }

                Console.WriteLine();
            }
        }


        private static long Factorial(long x)
        {
            if (x == 0 || x == 1)
            {
                return 1L;
            }

            return x * Factorial(x - 1);
        }

        private static long InverseFactorial(double value)
        {
            if (value > long.MaxValue)
            {
                throw new ArgumentException("too big");
            }

            long x = 1;
            while (Factorial(x) < value)
            {
                x++;
            }

            return x - 1;
        }
    }
}