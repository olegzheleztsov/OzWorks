namespace Oz.Algorithms.DataStructures.Trees
{
    public interface ITreeMaximumSearcher<T> where T : class, ITreeNode
    {
        T Maximum(T startNode);
    }
}