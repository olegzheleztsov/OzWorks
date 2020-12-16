using System;
using System.Threading.Tasks;

namespace Oz.Algorithms.DataStructures.Trees
{
    public interface ITreeWalker
    {
        void Walk(Action<ITreeNode> visitor);

        Task WalkAsync(Func<ITreeNode, Task> visitorAsync);
        
    }
}