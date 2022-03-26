// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Algorithms.Sedgewick.SearchTables;

public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
{
    public enum Color
    {
        Red,
        Black
    }

    private Node _root;

    public void Put(TKey key, TValue value)
    {
        if (key == null)
        {
            throw new ArgumentException("Key cannot be null");
        }

        if (value == null)
        {
            Delete(key);
            return;
        }
        _root = Put(_root, key, value);
        _root.Color = Color.Black;
    }

    public void Delete(TKey key)
    {
        if (key == null)
        {
            throw new ArgumentException("Key cannot be null");
        }

        if (IsEmpty || !Contains(key))
        {
            return;
        }

        if (!IsRed(_root.Left) && !IsRed(_root.Right))
        {
            _root.Color = Color.Red;
        }

        _root = Delete(_root, key);

        if (!IsEmpty)
        {
            _root.Color = Color.Black;
        }
    }

    private Node Delete(Node node, TKey key)
    {
        if (node == null)
        {
            return node;
        }

        if (key.CompareTo(node.Key) < 0)
        {
            if (!IsRed(node.Left) && node.Left != null && !IsRed(node.Left.Left))
            {
                node = MoveRedLeft(node);
            }

            node.Left = Delete(node.Left, key);
        }
        else
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }

            if (key.CompareTo(node.Key) == 0 && node.Right == null)
            {
                return null;
            }

            if (!IsRed(node.Right) && node.Right != null && !IsRed(node.Right.Left))
            {
                node = MoveRedRight(node);
            }

            if (key.CompareTo(node.Key) == 0)
            {
                var aux = Min(node.Right);
                node.Key = aux.Key;
                node.Value = aux.Value;
                node.Right = DeleteMin(node.Right);
            }
            else
            {
                node.Right = Delete(node.Right, key);
            }
        }

        return Balance(node);
    }

    public TKey Min()
    {
        if (_root == null)
        {
            throw new InvalidOperationException("Empty binary search tree");
        }

        return Min(_root).Key;
    }

    private Node Min(Node node) =>
        node.Left == null ? node : Min(node.Left);

    public void DeleteMin()
    {
        if (IsEmpty)
        {
            return;
        }

        if (!IsRed(_root.Left) && !IsRed(_root.Right))
        {
            _root.Color = Color.Red;
        }

        _root = DeleteMin(_root);

        if (!IsEmpty)
        {
            _root.Color = Color.Black;
        }
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return null;
        }

        if (!IsRed(node.Left) && !IsRed(node.Left.Left))
        {
            node = MoveRedLeft(node);
        }

        node.Left = DeleteMin(node.Left);
        return Balance(node);
    }

    private Node Balance(Node node)
    {
        if (node == null)
        {
            return null;
        }

        if (IsRed(node.Right) && !IsRed(node.Left))
        {
            node = RotateLeft(node);
        }

        if (IsRed(node.Left) && IsRed(node.Left.Left))
        {
            node = RotateRight(node);
        }
        if(IsRed(node.Left) && IsRed(node.Right))
        {
            FlipColors(node);
        }

        node.Size = Size(node.Left) + 1 + Size(node.Right);
        return node;
    }

    private Node MoveRedLeft(Node node)
    {
        FlipColors(node);
        if (node.Right != null && IsRed(node.Right.Left))
        {
            node.Right = RotateRight(node.Right);
            node = RotateLeft(node);
            FlipColors(node);
        }

        return node;
    }

    private Node MoveRedRight(Node node)
    {
        FlipColors(node);
        if (node.Left != null && IsRed(node.Left.Left))
        {
            node = RotateRight(node);
            FlipColors(node);
        }

        return node;
    }

    public TValue Get(TKey key)
    {
        if (key == null)
        {
            return default;
        }

        return Get(_root, key);
    }

    private TValue Get(Node node, TKey key)
    {
        if (node == null)
        {
            return default;
        }

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
        {
            return Get(node.Left, key);
        } else if (compare > 0)
        {
            return Get(node.Right, key);
        }
        else
        {
            return node.Value;
        }
    }

    public bool Contains(TKey key)
    {
        if (key == null)
        {
            throw new ArgumentException("Argument to contains() cannot be null");
        }

        return Get(key) != null;
    }

    public bool IsEmpty => Size(_root) == 0;

    public void Inorder(Action<Node> visit) => Inorder(_root, visit);

    private static void Inorder(Node node, Action<Node> visit)
    {
        if (node == null)
        {
            return;
        }

        Inorder(node.Left, visit);
        visit(node);
        Inorder(node.Right, visit);
    }

    private Node Put(Node h, TKey key, TValue value)
    {
        if (h == null)
        {
            return new Node(key, value, 1, Color.Red);
        }

        var cmp = key.CompareTo(h.Key);
        switch (cmp)
        {
            case < 0:
                h.Left = Put(h.Left, key, value);
                break;
            case > 0:
                h.Right = Put(h.Right, key, value);
                break;
            default:
                h.Value = value;
                break;
        }

        if (IsRed(h.Right) && !IsRed(h.Left))
        {
            h = RotateLeft(h);
        }

        if (IsRed(h.Left) && IsRed(h.Left.Left))
        {
            h = RotateRight(h);
        }

        if (IsRed(h.Left) && IsRed(h.Right))
        {
            FlipColors(h);
        }

        h.Size = Size(h.Left) + Size(h.Right) + 1;
        return h;
    }

    private Node RotateLeft(Node h)
    {
        if (h == null || h.Right == null)
        {
            return h;
        }
        var x = h.Right;
        h.Right = x.Left;
        x.Left = h;
        x.Color = h.Color;
        h.Color = Color.Red;
        x.Size = h.Size;
        h.Size = 1 + Size(h.Left) + Size(h.Right);
        return x;
    }

    private Node RotateRight(Node h)
    {
        if (h == null || h.Left == null)
        {
            return h;
        }
        
        var x = h.Left;
        h.Left = x.Right;
        x.Right = h;
        x.Color = h.Color;
        h.Color = Color.Red;
        x.Size = h.Size;
        h.Size = 1 + Size(h.Left) + Size(h.Right);
        return x;
    }

    private void FlipColors(Node node)
    {
        if (node is not {Left: { }, Right: { }})
        {
            return;
        }

        if ((IsRed(node) && !IsRed(node.Left) && !IsRed(node.Right)) ||
            (!IsRed(node) && IsRed(node.Left) && IsRed(node.Right)))
        {
            node.Color = GetFlippedColor(node.Color);
            node.Left.Color = GetFlippedColor(node.Left.Color);
            node.Right.Color = GetFlippedColor(node.Right.Color);
        }

        Color GetFlippedColor(Color color)
        {
            return color == Color.Red ? Color.Black : Color.Red;
        }
    }

    private static int Size(Node x) => x?.Size ?? 0;

    private bool IsRed(Node x)
    {
        if (x == null)
        {
            return false;
        }
        
        return x.Color == Color.Red;
    }

    public class Node
    {
        public Node(TKey key, TValue value, int size, Color color) =>
            (Key, Value, Size, Color) = (key, value, size, color);

        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }

        public override string ToString() =>
            $"({Key}: {Value}, Size:{Size}, Color:{Color}, L->{(Left != null ? Left.Key : "NULL")}, R->{(Right != null ? Right.Key : "NULL")})";
    }
}