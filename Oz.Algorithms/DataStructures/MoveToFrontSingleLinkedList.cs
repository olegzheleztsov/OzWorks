using System;

namespace Oz.Algorithms.DataStructures
{
    public class MoveToFrontSingleLinkedList<T> : OzSingleLinkedList<T>, ISelfOrganizedList<T>
    {
        public ISelfOrganizedListNode Access(Func<T, bool> condition)
        {
            var targetNode = Search(condition);
            if (targetNode != null)
            {
                Rearrange(targetNode);
            }
            return targetNode;
        }

        public void Rearrange(ISelfOrganizedListNode node)
        {
            var count = Count;
            if (count < 2)
            {
                return;
            }
            
            if (node is OzSingleLinkedListNode<T> targetNode)
            {
                var cellBefore = GetCellBefore(targetNode);
                if (cellBefore != null)
                {
                    cellBefore.Next = targetNode.Next;
                    if (targetNode == TailNode)
                    {
                        TailNode = cellBefore;
                    }

                    targetNode.Next = HeadNode;
                    HeadNode = targetNode;
                }
            }
            
        }
    }
}