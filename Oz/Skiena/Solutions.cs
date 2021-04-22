using System;
using System.Linq;
using Oz.Algorithms;
using Oz.Algorithms.Sort;

namespace Oz.Skiena
{
    public static class Solutions
    {
        /// <summary>
        /// Ex 4.2.a Let S be an unsorted array of n integers. Give an algorithm that finds the
        /// pair x, y ∈ S that maximizes |x−y|. Your algorithm must run in O(n) worst-case
        /// time.
        /// </summary>
        public static int GetMaximizeAbsDifference(this int[] array)
        {
            if (array == null)
            {
                return 0;
            }

            switch (array.Length)
            {
                case 1:
                    return 0;
                case 2:
                    return Math.Abs(array[1] - array[0]);
            }

            var minElement = array[0];
            var maxElement = array[0];
            for (var i = 1; i < array.Length; i++)
            {
                if (array[i] < minElement)
                {
                    minElement = array[i];
                }

                if (array[i] > maxElement)
                {
                    maxElement = array[i];
                }
            }

            return Math.Abs(maxElement - minElement);
        }

        /// <summary>
        /// Let S be an unsorted array of n integers. Give an algorithm that finds the
        /// pair x, y ∈ S that minimizes |x − y|, for x != y. Your algorithm must run in
        /// O(n log n) worst-case time.
        /// </summary>
        /// <returns></returns>
        public static int GetMinimizedAbsDifference(this int[] array)
        {
            if (array == null)
            {
                return 0;
            }

            switch (array.Length)
            {
                case 1:
                    return 0;
                case 2:
                    return Math.Abs(array[1] - array[0]);
            }

            var quickSorter = new QuickSorter<int>(PartitionStrategy.RandomizedPartition);
            quickSorter.Sort(array, element => element, Comparisions.StandardComparision);

            var currentMin = int.MaxValue;
            for (int i = 0, j = 1; j < array.Length; i++, j++)
            {
                if (Math.Abs(array[j] - array[i]) < currentMin)
                {
                    currentMin = Math.Abs(array[j] - array[i]);
                } 
            }

            return currentMin;
        }
        
        public static int countingValleys(int steps, string path)
        {
            if (steps == 0)
            {
                return 0;
            }

            var altitude = 0;
            var countOfValues = 0;
            for (var i = 0; i < steps; i++)
            {
                if (path[i] == 'D' && altitude == 0)
                {
                    countOfValues++;
                }

                altitude += (path[i] == 'U' ? 1 : -1);
            }

            return countOfValues;
        }
        
        static int getMoneySpent(int[] keyboards, int[] drives, int b)
        {
            var maxPrice = -1;
            foreach (var k in keyboards)
            {
                foreach (var d in drives)
                {
                    var price = k + d;
                    if (price <= b && price > maxPrice)
                    {
                        maxPrice = price;
                    }
                }
            }

            return maxPrice;
        }
    }
}