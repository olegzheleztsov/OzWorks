namespace Oz.Algorithms.DataStructures.Trees
{
    public class RecursiveMaximumTreeSearcher : ITreeMaximumSearcher
    {
        private readonly IBinaryTree _tree;

        public RecursiveMaximumTreeSearcher(IBinaryTree tree)
        {
            _tree = tree;
        }
        
        public ITreeNode Maximum(ITreeNode startNode)
        {
            return _tree.IsNull(startNode.RightChild) ? startNode : Maximum(startNode.RightChild);
        }
    }
}