namespace Oz.Algorithms.DataStructures
{
    public interface IDoubleLinkedNode<T>
    {
        public IDoubleLinkedNode<T> Next { get; set; }
        public IDoubleLinkedNode<T> Prev { get; set; }
        public T Data { get; }
    }
}