namespace Oz.Algorithms.DataStructures
{
    public class PriorityNode<T>
    {
        public PriorityNode(int priority, T data)
        {
            Priority = priority;
            Data = data;
        }

        public int Priority { get; set; }
        public T Data { get; }
    }
}