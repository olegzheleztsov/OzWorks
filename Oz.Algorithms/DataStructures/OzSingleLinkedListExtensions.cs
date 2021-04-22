namespace Oz.Algorithms.DataStructures
{
    public static class OzSingleLinkedListExtensions
    {
        public static OzSingleLinkedListNode<T> GetCircleStart<T>(this OzSingleLinkedListNode<T> head)
        {
            if (head?.Next == null)
            {
                return null;
            }

            if (head.Next == head)
            {
                return head;
            }

            if (head.Next.Next == head)
            {
                return head;
            }

            var sentinel = new OzSingleLinkedListNode<T>(default) {Next = head};

            var slowPointer = sentinel;
            var fastPointer = sentinel;
            
            while (true)
            {
                slowPointer = (OzSingleLinkedListNode<T>)slowPointer.Next;
                if (fastPointer.Next != null)
                {
                    fastPointer = (OzSingleLinkedListNode<T>)fastPointer.Next;
                }
                else
                {
                    return null;
                }

                if (fastPointer.Next != null)
                {
                    fastPointer = (OzSingleLinkedListNode<T>)fastPointer.Next;
                }
                else
                {
                    return null;
                }

                //found loop
                if (fastPointer == slowPointer)
                {
                    fastPointer = sentinel;
                    break;
                }
            }

            while (fastPointer != slowPointer)
            {
                fastPointer = (OzSingleLinkedListNode<T>)fastPointer.Next;
                slowPointer = (OzSingleLinkedListNode<T>)slowPointer.Next;
            }

            sentinel.Next = null;
            return fastPointer;
        }
    }
}