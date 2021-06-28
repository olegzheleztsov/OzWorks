namespace Oz.Algorithms.Rod.Trees
{
    public interface IBinaryNode<T, U>
    {
        public T LeftChild { get; set; }
        public T RightChild { get; set; }
        public U Data { get;  }
    }
}