namespace Oz.Algorithms.DataStructures.Trees
{
    public class TemplatedTreeMinimumSearcher<T> : ITreeMinimumSearcher<T> where T: class, ITreeNode
    {
        private readonly ITreeMinimumSearcher _searcher;

        public TemplatedTreeMinimumSearcher(ITreeMinimumSearcher searcher)
        {
            _searcher = searcher;
        }
        
        public T Minimum(T startNode = default(T))
        {
            return _searcher.Minimum(startNode) as T;
        }
    }
}