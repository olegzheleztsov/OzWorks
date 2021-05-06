using System;
using Oz.Algorithms.DataStructures;

namespace Oz.Algorithms.Rod.Sorting
{
    public static class LinkedListExtensions
    {
        public static int? FindSortedIndex<T>(this OzSingleLinkedList<T> list, T data, Comparison<T> comparison)
        {
            var index = 0;
            var pointer = list.HeadNode;

            while (pointer != null)
            {
                if (comparison(pointer.Data, data) == 0)
                {
                    return index;
                }

                if (comparison(pointer.Data, data) > 0)
                {
                    return null;
                }

                pointer = pointer.Next;
                index++;
            }

            return null;
        }
    }
}