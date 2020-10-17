using System;

namespace Oz.Algorithms.Sort
{
    public class MergeSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, SortDirection direction = SortDirection.Ascending)
        {
            if (array.Length < 2)
            {
                return;
            }
            
            MergeSort(array, 0, array.Length - 1, keySelector, direction);
        }

        private void MergeSort(T[] array, int p, int r, Func<T, int> keySelector, SortDirection direction)
        {
            if (p >= r) return;
            var q = (int)Math.Floor((double)(p + r) / 2);
            MergeSort(array, p, q, keySelector, direction);
            MergeSort(array, q + 1, r, keySelector, direction);
            Merge(array, p, q, r, keySelector, direction);
        }

        private void Merge(T[] array, int p, int q, int r, Func<T, int> keySelector, SortDirection direction)
        {
            var nLeft = q - p + 1;
            var nRight = r - q;
            var leftSubArray = new T[nLeft];
            var rightSubArray = new T[nRight];
            for (var i = 0; i < nLeft; i++)
            {
                leftSubArray[i] = array[p + i];
            }

            for (var j = 0; j < nRight; j++)
            {
                rightSubArray[j] = array[q + j + 1];
            }

            int mergeIndexLeft = 0, mergeIndexRight = 0;
            var k = p;
            for (; k <= r; k++)
            {
                if (mergeIndexLeft == nLeft || mergeIndexRight == nRight)
                {
                    break;
                }

                if (IsLessOrEqual(keySelector(leftSubArray[mergeIndexLeft]),
                    keySelector(rightSubArray[mergeIndexRight]), direction))
                {
                    array[k] = leftSubArray[mergeIndexLeft++];
                }
                else
                {
                    array[k] = rightSubArray[mergeIndexRight++];
                }
            }

            if (mergeIndexRight == nRight)
            {
                for (var tailIndex = mergeIndexLeft; tailIndex < nLeft; tailIndex++)
                {
                    array[k++] = leftSubArray[tailIndex];
                }
            }
            else if (mergeIndexLeft == nLeft)
            {
                for (var tailIndex = mergeIndexRight; tailIndex < nRight; tailIndex++)
                {
                    array[k++] = rightSubArray[tailIndex];
                }
            }
        }

        private bool IsLessOrEqual(int first, int second, SortDirection direction)
        {
            var result = first.CompareTo(second);
            return IsLess(first, second, direction) || result == 0;
        }

        private bool IsLess(int first, int second, SortDirection direction)
        {
            return first.CompareTo(second) == ComparisionSign(direction);
        }

        private int ComparisionSign(SortDirection direction)
        {
            return direction == SortDirection.Ascending ? -1 : 1;
        }
    }
}