namespace Oz.Algorithms.DataStructures.Trees
{
    public interface ITreeSearcher<out T> where T : class, ITreeNode
    {
        T Search(int key);
    }
}