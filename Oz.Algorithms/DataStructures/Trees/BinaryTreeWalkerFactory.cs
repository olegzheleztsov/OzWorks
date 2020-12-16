using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public static class BinaryTreeWalkerFactory
    {
        public static ITreeWalker Create(IBinaryTree tree, TreeWalkStrategy strategy)
        {
            return strategy switch
            {
                TreeWalkStrategy.Inorder => new InorderTreeWalker(tree),
                TreeWalkStrategy.Postorder => new PostorderTreeWalker(tree),
                TreeWalkStrategy.Preorder => new PreorderTreeWalker(tree),
                _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
            };
        }
    }
}