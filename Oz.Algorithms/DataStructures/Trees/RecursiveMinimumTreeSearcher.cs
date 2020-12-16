namespace Oz.Algorithms.DataStructures.Trees
{
    public class RecursiveMinimumTreeSearcher : ITreeMinimumSearcher
    {
        private readonly IBinaryTree _tree;

        public RecursiveMinimumTreeSearcher(IBinaryTree tree)
        {
            _tree = tree;
        }

        public ITreeNode Minimum(ITreeNode startNode = null)
        {
            return _Minimum(startNode ?? _tree.Root);
        }

        private ITreeNode _Minimum(ITreeNode parentNode)
        {
            return _tree.IsNull(parentNode.LeftChild) ? parentNode : _Minimum(parentNode.LeftChild);
        }
    }
}