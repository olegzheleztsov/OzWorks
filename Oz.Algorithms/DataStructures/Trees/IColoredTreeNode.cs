namespace Oz.Algorithms.DataStructures.Trees
{
    public interface IColoredTreeNode : ITreeNode
    {
        TreeNodeColor Color { get; set; }
    }
}