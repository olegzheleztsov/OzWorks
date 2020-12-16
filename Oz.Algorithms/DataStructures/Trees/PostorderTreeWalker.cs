﻿using System;
using System.Threading.Tasks;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class PostorderTreeWalker : ITreeWalker
    {
        private readonly IBinaryTree _tree;

        public PostorderTreeWalker(IBinaryTree tree)
        {
            _tree = tree;
        }
        
        public void Walk(Action<ITreeNode> visitor)
        {
            if (_tree == null)
            {
                return;
            }

            _Walk(_tree.Root, visitor);
        }

        public async Task WalkAsync(Func<ITreeNode, Task> visitorAsync)
        {
            if (_tree == null)
            {
                return;
            }

            await _WalkAsync(_tree.Root, visitorAsync).ConfigureAwait(false);
        }
        
        private void _Walk(ITreeNode node, Action<ITreeNode> visitor)
        {
            if (!_tree.IsNull(node))
            {
                _Walk(node.LeftChild, visitor);
                _Walk(node.RightChild, visitor);
                visitor?.Invoke(node);
            }
        }

        private async Task _WalkAsync(ITreeNode node, Func<ITreeNode, Task> visitorAsync)
        {
            if (!_tree.IsNull(node))
            {
                await _WalkAsync(node.LeftChild, visitorAsync).ConfigureAwait(false);
                await _WalkAsync(node.RightChild, visitorAsync).ConfigureAwait(false);
                await visitorAsync(node).ConfigureAwait(false);
            }
        }      
    }
}