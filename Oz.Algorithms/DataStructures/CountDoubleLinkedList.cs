#region

using System;
using System.Text;

#endregion

namespace Oz.Algorithms.DataStructures
{
    public class CountDoubleLinkedList<T> : OzDoubleLinkedList<FrequencyData<T>>, ISelfOrganizedList<FrequencyData<T>>
    {
        public ISelfOrganizedListNode Access(Func<FrequencyData<T>, bool> condition)
        {
            var node = Search(condition);
            if (node != null)
            {
                node.Data.Frequency++;
                Rearrange(node);
            }

            return node;
        }

        private void Rearrange(OzDoubleLinkedListNode<FrequencyData<T>> node)
        {
            var pointer = node.Prev;

            while (pointer != null && pointer.Data.Frequency < node.Data.Frequency)
            {
                pointer = ((OzDoubleLinkedListNode<FrequencyData<T>>) pointer).Prev;
            }

            if (pointer != null)
            {
                if (node.Prev != null)
                {
                    node.Prev.Next = node.Next;
                }

                if (node.Next != null)
                {
                    ((OzDoubleLinkedListNode<FrequencyData<T>>) node.Next).Prev = node.Prev;
                }

                node.Next = pointer.Next;
                node.Prev = pointer;
                pointer.Next = node;
                if (node.Next != null)
                {
                    ((OzDoubleLinkedListNode<FrequencyData<T>>) node.Next).Prev = node;
                }
            }
            else
            {
                if (node.Data.Frequency > HeadNode.Data.Frequency)
                {
                    node.Prev.Next = node.Next;
                    ((OzDoubleLinkedListNode<FrequencyData<T>>) node.Next).Prev = node.Prev;
                    node.Prev = null;
                    node.Next = HeadNode;
                    ((OzDoubleLinkedListNode<FrequencyData<T>>) HeadNode).Prev = node;
                    HeadNode = node;
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[ ");
            foreach (var data in this)
            {
                stringBuilder.Append($"f: {data.Frequency}, d: {data.Data}, ");
            }

            if (stringBuilder.Length > 2)
            {
                stringBuilder.Length = stringBuilder.Length - 2;
            }

            stringBuilder.Append("] ");
            return stringBuilder.ToString();
        }
    }
}