namespace Oz.Algorithms.DataStructures
{
    public interface IOzLinkedList<T>
    {
        public IListNode<T> HeadNode { get; }
        public IListNode<T> TailNode { get; }

        bool IsEmpty { get; }

        int Count { get; }
    }
}