using System;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    public class FibonacciHeapNode<T> : IDoubleLinkedNode<T> where T : IKey
    {
        public FibonacciHeapNode(T data)
        {
            Data = data;
        }

        public FibonacciHeapNode<T> Parent { get; set; }

        public OzDoubleCyclicLinkedList<T> Child { get; set; }
        
        
        public int Degree { get; set; }

        public bool IsMark { get; set; }

        public IDoubleLinkedNode<T> Next { get; set; }
        public IDoubleLinkedNode<T> Prev { get; set; }
        public T Data { get; }

        public void Preorder(Action<FibonacciHeapNode<T>> visitor, FibonacciHeapNode<T> currentNode)
        {
            if (currentNode != null)
            {
                visitor?.Invoke(currentNode);
            }
            else
            {
                return;
            }

            if (currentNode.Child != null)
            {
                foreach (var node in currentNode.Child.EnumerateNodes())
                {
                    Preorder(visitor, node as FibonacciHeapNode<T>);
                }
            }
        }


        public override string ToString()
        {
            
            var stringBuilder = new StringBuilder();
            Preorder((node) =>
            {

                stringBuilder.AppendLine("START ==>");
                stringBuilder.AppendLine($"DATA: {node.Data?.ToString() ?? "<NONE>"}  DEG: {node.Degree} IS_MARK: {node.IsMark}");
                if (node.Child != null)
                {
                    stringBuilder.AppendLine($"CHILDREN: {string.Join(" ", node.Child)}");
                }

                stringBuilder.AppendLine("END <==");
            }, this);

            return stringBuilder.ToString();
        }
    }
}