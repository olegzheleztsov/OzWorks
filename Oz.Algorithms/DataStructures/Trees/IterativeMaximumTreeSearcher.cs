namespace Oz.Algorithms.DataStructures.Trees
{
    public class IterativeMaximumTreeSearcher : ITreeMaximumSearcher
    {
        private readonly IBinaryTree _tree;

        public IterativeMaximumTreeSearcher(IBinaryTree tree)
        {
            _tree = tree;
        }
        
        public ITreeNode Maximum(ITreeNode startNode)
        {
            if (_tree.IsNull(startNode))
            {
                return null;
            }

            while (!_tree.IsNull(startNode.RightChild))
            {
                startNode = startNode.RightChild;
            }

            return startNode;
        }
    }
}