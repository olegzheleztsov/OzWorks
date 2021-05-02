#nullable enable
namespace Oz.Algorithms.DataStructures
{
    public static class OzLinkedListBasedStackExtensions
    {
        public static OzLinkedListBasedStack<T> Reversed<T>(this OzLinkedListBasedStack<T> stack)
        {
            OzLinkedListBasedStack<T> reversedStack = new();
            while (!stack.IsEmpty)
            {
                reversedStack.Push(stack.Pop());
            }

            return reversedStack;
        }
    }
}