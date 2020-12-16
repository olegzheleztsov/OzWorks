namespace Oz.Algorithms.DataStructures.Trees
{
    public class IterativeMinimumTreeSearcher : ITreeMinimumSearcher
    {
        private readonly IBinaryTree _tree;

        public IterativeMinimumTreeSearcher(IBinaryTree tree)
        {
            _tree = tree;
        }
        
        public ITreeNode Minimum(ITreeNode startNode = null)
        {
            return _Minimum(startNode ?? _tree.Root);
        }

        private ITreeNode _Minimum(ITreeNode node)
        {
            while (!_tree.IsNull(node.LeftChild))
            {
                node = node.LeftChild;
            }

            return node;
        }
    }
}