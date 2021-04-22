namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleLinkedListNode<T> : IListNode<T>, ISelfOrganizedListNode
    {
        public OzDoubleLinkedListNode(T data)
        {
            Data = data;
        }

        public IListNode<T> Next { get; set; }
        public IListNode<T> Prev { get; set; }
        public T Data { get; }
    }
}