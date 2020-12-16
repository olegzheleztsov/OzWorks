using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public static class BinaryTreeSearcherFactory
    {
        public static ITreeSearcher Create(IBinaryTree tree, Func<ITreeNode, int> keySelector, SearchMethod searchMethod)
        {
            return searchMethod switch
            {
                SearchMethod.Iterative => new IterativeBinaryTreeSearcher(tree, keySelector),
                SearchMethod.Recursive => new RecursiveBinaryTreeSearcher(tree, keySelector),
                _ => throw new ArgumentOutOfRangeException(nameof(searchMethod), searchMethod, null)
            };
        }

        public static ITreeSearcher<T> Create<T>(IBinaryTree tree, Func<T, int> keySelector, SearchMethod searchMethod)
            where T : class, ITreeNode
        {
            var searcher = Create(tree, node => keySelector((T) node), searchMethod);
            var templatedSearcher = new TemplatedTreeSearcher<T>(searcher);
            return templatedSearcher;
        }
    }
}