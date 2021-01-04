using System;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms
{
    public static class Util
    {
        public const float Eps = 1e-3f;
        
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
            else
            {
                return 1;
            }
        }
    }
}