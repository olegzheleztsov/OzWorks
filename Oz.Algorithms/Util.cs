using System;

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

        public static bool IsPowerOf2(int value)
        {
            return value != 0 && (value & (value - 1)) == 0;
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
    }
}