namespace Oz.Algorithms.DataStructures
{
    public interface IListNode<T>
    {
        T Data { get; }
        
        public IListNode<T> Next { get; set; }
    }
}