using System;
using System.Numerics;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms
{
    public static class Util
    {
        public const float Eps = 1e-3f;
        public const int IntegerPositiveInfinity = int.MaxValue;
        public const int IntegerNegativeInfinity = int.MinValue;

        public static void Exchange<T>(ref T first, ref T second)
        {
            var temp = first;
            first = second;
            second = temp;
        }

        public static int Compare(float f1, float f2)
        {
            if (MathF.Abs(f1 - f2) < Eps)
            {
                return 0;
            }

            if (f1 < f2)
            {
                return -1;
            }

            return 1;
        }

        public static bool Approximately(float first, float second, float eps = Eps)
        {
            return MathF.Abs(first - second) < eps;
        }

        public static bool IsPowerOf2(int value)
        {
            return value != 0 && (value & (value - 1)) == 0;
        }

        /// <summary>
        /// Returns the next power of 2 that greater than number
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns>Power of 2 that greater than input number</returns>
        public static int NextPowerOf2(int number)
        {
            var curPower = 1;
            while (curPower < number)
            {
                curPower *= 2;
            }

            return curPower;
        }
        
        public static bool IsInfinity(int value)
        {
            return value == IntegerPositiveInfinity ||
                   value == IntegerNegativeInfinity;
        }

        public static bool IsPositiveInfinity(int value) => value == IntegerPositiveInfinity;
        public static bool IsNegativeInfinity(int value) => value == IntegerNegativeInfinity;

        public static string GetValueWithInf(this int value)
        {
            if (value == IntegerPositiveInfinity)
            {
                return "+INF";
            }

            if (value == IntegerNegativeInfinity)
            {
                return "-INF";
            }

            return value.ToString();
        }

        public static Complex RootOfUnit(int n, int degree)
        {
            return new Complex(Math.Cos(2.0 * Math.PI * degree / n), Math.Sin(2.0 * Math.PI * degree / n));
        }
    }
}