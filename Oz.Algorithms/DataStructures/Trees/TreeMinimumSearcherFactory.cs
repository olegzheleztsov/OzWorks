using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public static class TreeMinimumSearcherFactory
    {
        public static ITreeMinimumSearcher Create(IBinaryTree tree, SearchMethod searchMethod)
        {
            return searchMethod switch
            {
                SearchMethod.Iterative => new IterativeMinimumTreeSearcher(tree),
                SearchMethod.Recursive => new RecursiveMinimumTreeSearcher(tree),
                _ => throw new ArgumentOutOfRangeException(nameof(searchMethod), searchMethod, null)
            };
        }

        public static ITreeMinimumSearcher<T> Create<T>(IBinaryTree tree, SearchMethod searchMethod)
            where T : class, ITreeNode
        {
            var searcher = Create(tree, searchMethod);
            var templatedSearcher = new TemplatedTreeMinimumSearcher<T>(searcher);
            return templatedSearcher;
        }
    }
}