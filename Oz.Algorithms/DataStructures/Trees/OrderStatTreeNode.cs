namespace Oz.Algorithms.DataStructures.Trees
{
    public class OrderStatTreeNode<T> : BinaryTreeNode<T>, IColoredTreeNode
    {
        private readonly bool _isNull;

        public OrderStatTreeNode(T data, TreeNodeColor color = TreeNodeColor.Black, bool isNull = false) :
            base(data)
        {
            Color = color;
            _isNull = isNull;
        }


        public int Size { get; private set; }
        public TreeNodeColor Color { get; set; }

        public int UpdateSize()
        {
            if (_isNull)
            {
                return 0;
            }

            var left = Left as OrderStatTreeNode<T>;
            var right = Right as OrderStatTreeNode<T>;
            var leftSize = left?.UpdateSize() ?? 0;
            var rightSize = right?.UpdateSize() ?? 0;
            Size = leftSize + rightSize + 1;
            return Size;
        }
    }
}