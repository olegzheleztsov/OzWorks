namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     Node for single linked list
    /// </summary>
    /// <typeparam name="T">Type of single linked list data</typeparam>
    public class OzSingleLinkedListNode<T>
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
        public OzSingleLinkedListNode<T> Next { get; set; }

        /// <summary>
        ///     Node's data
        /// </summary>
        public T Data { get; }
    }
}