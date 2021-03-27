using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class TreeMaximumSearcherFactory
    {
        public static ITreeMaximumSearcher Create(IBinaryTree tree, SearchMethod searchMethod = SearchMethod.Recursive)
        {
            return searchMethod switch
            {
                SearchMethod.Iterative => new IterativeMaximumTreeSearcher(tree),
                SearchMethod.Recursive => new RecursiveMaximumTreeSearcher(tree),
                _ => throw new ArgumentOutOfRangeException(nameof(searchMethod), searchMethod, null)
            };
        }


        public static ITreeMaximumSearcher<T> Create<T>(IBinaryTree tree,
            SearchMethod searchMethod = SearchMethod.Recursive) where T : class, ITreeNode
        {
            var searcher = Create(tree, searchMethod);
            var templatedSearcher = new TemplatedTreeMaximumSearcher<T>(searcher);
            return templatedSearcher;
        }
    }
}