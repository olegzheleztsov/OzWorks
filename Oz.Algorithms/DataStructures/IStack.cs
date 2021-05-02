#nullable enable
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public interface IStack<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        
        void Push(T value);

        T Peek();

        T Pop();
        
    }
}