namespace Oz.Algorithms.DataStructures.Trees
{
    public class TemplatedTreeMaximumSearcher<T> : ITreeMaximumSearcher<T> where T : class, ITreeNode
    {
        private readonly ITreeMaximumSearcher _searcher;

        public TemplatedTreeMaximumSearcher(ITreeMaximumSearcher searcher)
        {
            _searcher = searcher;
        }


        public T Maximum(T startNode)
        {
            return _searcher.Maximum(startNode) as T;
        }
    }
}