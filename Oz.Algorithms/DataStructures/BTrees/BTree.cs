using System;
using System.Text;

namespace Oz.Algorithms.DataStructures.BTrees
{
    public class BTree<T>
    {
        private readonly Func<int, BTreeNode<T>> _allocate;
        private readonly Action<BTreeNode<T>> _diskWrite;
        private readonly Func<BTree<T>, BTreeNode<T>, int, BTreeNode<T>> _diskRead;
        private readonly int _treeDegree;

        public BTree(Func<int, BTreeNode<T>> allocate, 
            Action<BTreeNode<T>> diskWrite, 
            Func<BTree<T>, BTreeNode<T>, int, BTreeNode<T>> diskRead, 
            int treeDegree)
        {
            _allocate = allocate;
            _diskWrite = diskWrite;
            _diskRead = diskRead;
            _treeDegree = treeDegree;
            CreateTree();
        }

        public int TreeDegree => _treeDegree;

        public BTreeNode<T> CreateNode(bool isLeaf, T data)
        {
            var node = _allocate(_treeDegree);
            node.IsLeaf = isLeaf;
            if (node.IsLeaf)
            {
                node.KeyCount = 0;
            }
            else
            {
                node.KeyCount = _treeDegree - 1;
            }

            node.Data = data;
            return node;
        }

        public BTreeNode<T> Root { get; private set; }

        private void CreateTree()
        {
            var x = _allocate(_treeDegree);
            x.IsLeaf = true;
            x.KeyCount = 0;
            _diskWrite(x);
            Root = x;
        }

        public void Insert(int key)
        {
            var r = Root ?? throw new NullReferenceException(nameof(Root));
            if (r.KeyCount == 2 * _treeDegree - 1)
            {
                var s = _allocate(_treeDegree);
                Root = s;
                s.IsLeaf = false;
                s.KeyCount = 0;
                s.Children[0] = r;
                SplitChild(s, 0);
                InsertNonFull(s, key);
            }
            else
            {
                InsertNonFull(r, key);
            }
        }

        private void InsertNonFull(BTreeNode<T> x, int k)
        {
            if (x == null)
            {
                throw new NullReferenceException(nameof(x));
            }
            
            var i = x.KeyCount - 1;
            if (x.IsLeaf)
            {
                x.KeyCount++;
                while (i >= 0 && k < x.Keys[i])
                {
                    x.Keys[i + 1] = x.Keys[i];
                    i--;
                }

                x.Keys[i + 1] = k;
                _diskWrite(x);
            }
            else
            {
                while (i >= 0 && k < x.Keys[i])
                {
                    i--;
                }

                i++;
                x.Children[i] = _diskRead(this, x, i);

                var childI = x.Children[i] ?? throw new NullReferenceException(nameof(x.Children));
                if (childI.KeyCount == 2 * _treeDegree - 1)
                {
                    SplitChild(x, i);
                    if (k > x.Keys[i])
                    {
                        i++;
                    }
                }
                InsertNonFull(x.Children[i], k);
            }
        }

        public void SplitChild(BTreeNode<T> x, int index)
        {
            var z = _allocate(_treeDegree);
            var y = x.Children[index] ?? throw new NullReferenceException(nameof(x.Children));
            z.IsLeaf = y.IsLeaf;
            z.KeyCount = _treeDegree - 1;
            for (var j = 0; j < _treeDegree - 1; j++)
            {
                z.Keys[j] = y.Keys[j + _treeDegree];
            }

            if (!y.IsLeaf)
            {
                for (var j = 0; j < _treeDegree; j++)
                {
                    z.Children[j] = y.Children[j + _treeDegree];
                }
            }

            var yTKey = y.Keys[_treeDegree - 1];
            y.KeyCount = _treeDegree - 1;
            var oldXCount = x.KeyCount;
            x.KeyCount++;
            for (var j = oldXCount; j >= index + 1; j--)
            {
                x.Children[j + 1] = x.Children[j]; 
            }

            x.Children[index + 1] = z;
            for (var j = oldXCount - 1; j >= index; j--)
            {
                x.Keys[j + 1] = x.Keys[j];
            }

            x.Keys[index] = yTKey;
            _diskWrite(y);
            _diskWrite(z);
            _diskWrite(x);
        }

        public override string ToString()
        {
            if (Root == null)
            {
                return "<NONE>";
            }
            else
            {
                var stringBuilder = new StringBuilder();
                Preorder((node) =>
                {
                    stringBuilder.AppendLine(node.ToString());
                    stringBuilder.AppendLine();
                }, Root);
                return stringBuilder.ToString();
            }
        }

        private void Preorder(Action<BTreeNode<T>> visitor, BTreeNode<T> parent)
        {
            visitor?.Invoke(parent);
            foreach (var child in parent.Children)
            {
                if (child != null)
                {
                    Preorder(visitor, child);
                }
            }
        }
    }
}