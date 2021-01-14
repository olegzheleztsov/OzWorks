namespace Oz.Algorithms.DataStructures
{
    public static class Allocators
    {
        public static IDoubleLinkedNode<T> DoubleLinkedNodeAllocator<T>(T data)
        {
            var node = new DoubleLinkedNode<T>(data);
            return node;
        }
    }
}