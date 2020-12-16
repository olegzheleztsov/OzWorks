namespace Oz.Algorithms.DataStructures.Trees
{
    public interface IBinaryTree
    {
        ITreeNode Root { get; }

        bool IsNull(ITreeNode node);
    }
}