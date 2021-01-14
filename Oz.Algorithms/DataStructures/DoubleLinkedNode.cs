namespace Oz.Algorithms.DataStructures
{
    public class DoubleLinkedNode<T> : IDoubleLinkedNode<T>
    {
        public DoubleLinkedNode(T data)
        {
            Data = data;
        }
        
        public IDoubleLinkedNode<T> Next { get; set; }
        public IDoubleLinkedNode<T> Prev { get; set; }
        public T Data { get; }
    }
}