using System;

namespace Oz.Algorithms.DataStructures
{
    public static class VanEmdeBoasUtils
    {
        public static int UpperSquareRoot(int size)
        {
            var floorDegree = (int) Math.Floor(Math.Log2(size) / 2.0);
            return 1 << floorDegree;
        }

        public static int LowerSquareRoot(int size)
        {
            var ceilDegree = (int) Math.Ceiling(Math.Log2(size) / 2.0);
            return 1 << ceilDegree;
        }

        public static int High(int key, int size)
        {
            return (int) Math.Floor((double) key / LowerSquareRoot(size));
        }

        public static int Low(int key, int size)
        {
            return key % LowerSquareRoot(size);
        }

        public static int Index(int key, int val, int size)
        {
            return key * LowerSquareRoot(size) + val;
        }
    }
}