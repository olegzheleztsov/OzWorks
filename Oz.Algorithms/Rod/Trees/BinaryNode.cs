namespace Oz.Algorithms.Rod.Trees
{
    public class BinaryNode<T>
    {
        public BinaryNode(T data)
            => Data = data;
        
        public T Data { get; }
        
        public BinaryNode<T> LeftChild { get; set; }
        public BinaryNode<T> RightChild { get; set; }

        public void SetLeftRight(BinaryNode<T> left, BinaryNode<T> right)
            => (LeftChild, RightChild) = (left, right);
    }
}