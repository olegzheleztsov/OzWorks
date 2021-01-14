using System;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.DataStructures.BTrees
{
    public sealed class BTreeNode<T>
    {
        
        public BTreeNode(bool isLeaf, T data)
        {
            IsLeaf = isLeaf;
            Keys = new int[] { };
            Children = new BTreeNode<T>[] { };
            Data = data;
        }

        public T Data { get; set; }
        public int[] Keys { get; private set; }

        public BTreeNode<T>[] Children { get; private set; }
        public bool IsLeaf { get; set; }

        private void CopyKeys(int value)
        {
            var oldKeys = Keys;
            var newKeys = new int[value];

            for (var i = 0; i < value; i++)
            {
                if (i < oldKeys.Length)
                {
                    newKeys[i] = oldKeys[i];
                }
                else
                {
                    newKeys[i] = int.MinValue;
                }
            }
            Keys = newKeys;
        }

        private void CopyChildren(int value)
        {
            var oldChildren = Children;
            var newChildren = new BTreeNode<T>[value + 1];

            for (var i = 0; i < (value + 1); i++)
            {
                if (i < oldChildren.Length)
                {
                    newChildren[i] = oldChildren[i];
                }
                else
                {
                    newChildren[i] = null;
                }
            }

            Children = newChildren;
        }

        public int KeyCount
        {
            get => Keys.Length;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Value can't less than zero. Actual value: {value}");
                }

                if (value == Keys.Length && (value + 1) == Children.Length)
                {
                    return;
                }

                CopyKeys(value);
                CopyChildren(value);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Data: {Data?.ToString() ?? "<NO DATA>"}");
            stringBuilder.AppendLine($"Key count: {KeyCount}");
            stringBuilder.AppendLine($"Keys: {string.Join(" ", Keys)}");
            stringBuilder.AppendLine(
                $"Refs: {string.Join(" ", Children.Select(c => c == null ? "<NONE>" : (c.Data?.ToString() ?? "<NO DATA>")))}");
            return stringBuilder.ToString();
        }
    }
}