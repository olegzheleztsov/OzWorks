using System;

namespace Oz.Algorithms.DataStructures
{
    public class SwapSingleLinkedList<T> : OzSingleLinkedList<T>, ISelfOrganizedList<T>
    {
        public ISelfOrganizedListNode Access(Func<T, bool> condition)
        {
            var (prev, node) = SearchNode(condition);
            if (node != null)
            {
                Rearrange(prev, node);
            }

            return node;
        }

        private void Rearrange(OzSingleLinkedListNode<T> previousNode, OzSingleLinkedListNode<T> node)
        {
            if (previousNode != null)
            {
                var previousPreviousNode = GetCellBefore(previousNode);
                previousNode.Next = node.Next;
                node.Next = previousNode;
                if (node == TailNode)
                {
                    TailNode = previousNode;
                }
                if (previousPreviousNode != null)
                {
                    previousPreviousNode.Next = node;
                }
                else
                {
                    HeadNode = node;
                }
            }
        }
    }
}