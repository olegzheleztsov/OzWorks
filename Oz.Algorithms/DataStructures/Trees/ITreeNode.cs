namespace Oz.Algorithms.DataStructures.Trees
{
    public interface ITreeNode
    {
        ITreeNode LeftChild { get;}
        ITreeNode RightChild { get; }
        
        ITreeNode ParentNode { get; }
    }
}