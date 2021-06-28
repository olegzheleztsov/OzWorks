namespace Oz.Algorithms.Rod.Trees
{
    public class BinaryNodeParentedDepth<T> : IBinaryNode<BinaryNodeParentedDepth<T>, T>
    {
        public BinaryNodeParentedDepth(T data)
            => Data = data;
        
        public T Data { get; }
        
        public BinaryNodeParentedDepth<T> LeftChild { get; set; }
        public BinaryNodeParentedDepth<T> RightChild { get; set; }
        public BinaryNodeParentedDepth<T> Parent { get; set; }
        public int Depth { get; private set; }

        public void UpdateDepth()
        {
            if (Parent == null)
            {
                Depth = 0;
            }
            else
            {
                Depth = Parent.Depth + 1;
            }
            LeftChild?.UpdateDepth(); 
            RightChild?.UpdateDepth();
        }
    }
}