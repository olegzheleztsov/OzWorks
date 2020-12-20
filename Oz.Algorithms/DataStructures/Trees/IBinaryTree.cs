using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public interface IBinaryTree
    {
        ITreeNode Root { get; set; }

        bool IsNull(ITreeNode node);
        
        ITreeNode NullNode { get; }
        
        Func<object, int> KeySelector { get; }
    }
}