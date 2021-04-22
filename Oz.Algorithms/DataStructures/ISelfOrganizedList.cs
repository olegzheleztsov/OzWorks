using System;

namespace Oz.Algorithms.DataStructures
{
    public interface ISelfOrganizedList<out T>
    {
        ISelfOrganizedListNode Access(Func<T, bool> condition);
        
    }
}