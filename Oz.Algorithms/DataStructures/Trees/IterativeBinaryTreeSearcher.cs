using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class IterativeBinaryTreeSearcher : ITreeSearcher
    {
        private readonly Func<ITreeNode, int> _keySelector;
        private readonly IBinaryTree _tree;

        public IterativeBinaryTreeSearcher(IBinaryTree tree, Func<ITreeNode, int> keySelector)
        {
            _tree = tree;
            _keySelector = keySelector;
        }
        
        public ITreeNode Search(int key)
        {
            return _tree == null ? default : _Search(_tree.Root, key);
        }

        private ITreeNode _Search(ITreeNode node, int key)
        {
            while (!_tree.IsNull(node) && key != _keySelector(node))
            {
                node = key < _keySelector(node) ? node.LeftChild : node.RightChild;
            }

            return node;
        }
    }
}