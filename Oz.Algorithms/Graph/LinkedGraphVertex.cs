using Oz.Algorithms.DataStructures;

namespace Oz.Algorithms.Graph
{
    public class LinkedGraphVertex<T> : GraphVertex<T>
    {
        public LinkedGraphVertex(int index, T data)
            : base(index, data)
        {
            LinkedVertices = new OzSingleLinkedList<LinkedGraphVertex<T>>();
        }

        public OzSingleLinkedList<LinkedGraphVertex<T>> LinkedVertices { get; }

        public void AddLinkedVertex(LinkedGraphVertex<T> vertex)
        {
            LinkedVertices.InsertLast(vertex);
        }
    }
}