using System;
using Oz.Algorithms.Numerics;

namespace Oz
{
    public class MinimumMaximumCase
    {
        public void Run()
        {
            int[] arrIntegers = {3, 4, 5, 6};
            float[] arrFloats = {1.5f, 0.5f, 4.5f, 6.8f};
            double[] arrDoubles = {1.4, 0.5, 0.4, 2.3};
            
            Console.WriteLine($"min integer: {arrIntegers.Minimum()}, max integer: {arrIntegers.Maximum()}");
            Console.WriteLine($"min float: {arrFloats.Minimum()}, max float: {arrFloats.Maximum()}");
            Console.WriteLine($"min double: {arrDoubles.Minimum()}, max double: {arrDoubles.Maximum()}");
        }
    }
}