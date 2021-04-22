namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     OzDoubleLinkedListNode for single linked list
    /// </summary>
    /// <typeparam name="T">Type of single linked list data</typeparam>
    public class OzSingleLinkedListNode<T> : IListNode<T>, ISelfOrganizedListNode
    {
        public OzSingleLinkedListNode() : this(default)
        {
        }

        public OzSingleLinkedListNode(T data)
        {
            Data = data;
        }

        /// <summary>
        ///     Reference to next node
        /// </summary>
        public IListNode<T> Next { get; set; }

        /// <summary>
        ///     OzDoubleLinkedListNode's data
        /// </summary>
        public T Data { get; }
    }
}