using System;

namespace Oz.Algorithms.DataStructures
{
    public static class OzLinkedListExtensions
    {
        public static bool IsSorted<T>(this IOzLinkedList<T> list, Comparison<T> comparison)
        {
            if (list.IsEmpty || list.Count == 1)
            {
                return true;
            }

            var current = list.HeadNode;
            var currentValue = current.Data;

            current = current.Next;
            while (current != list.TailNode)
            {
                if (comparison(currentValue, current.Data) > 0)
                {
                    return false;
                }

                currentValue = current.Data;
                current = current.Next;
            }

            return comparison(currentValue, current.Data) <= 0;
        }
    }
}