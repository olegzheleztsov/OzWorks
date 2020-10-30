using System;
using System.Threading.Tasks;
using Oz.Algorithms.Numbers;

namespace Oz
{
    public class PascalTriangle : IExecutor
    {
        public async Task RunAsync(object arg)
        {
            await Task.Run(() =>
            {
                var n = (int) arg;
                for (var k = 0; k <= n; k++)
                {
                    Console.Write($"\t\t{k}");
                }

                Console.WriteLine();
                for (var iN = 0; iN < n; iN++)
                {
                    Console.Write($"{iN}\t\t");
                    for (var k = 0; k <= iN; k++)
                    {
                        var binCoef = new BinomialCoefficient(k, iN);
                        Console.Write($"{binCoef.Value}\t\t");
                    }

                    Console.WriteLine();
                }
            }).ConfigureAwait(false);
        }

        public static object ParseArguments(string[] args)
        {
            if (args?.Length > 0)
            {
                if (int.TryParse(args[0], out var size))
                {
                    return size;
                }
            }

            throw new ArgumentException("Cannot convert argument to integer");
        }
    }
}