// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sedgewick.SearchTables;

public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private Node Root;

    public int Size => SizeInner(Root);

    public int Height() =>
        Height(Root);

    private int Height(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private int SizeInner(Node node) =>
        node?.SubtreeCount ?? 0;

    public TValue Get(TKey key) =>
        Get(Root, key);

    private static TValue Get(Node node, TKey key)
    {
        if (node == null)
        {
            return default;
        }

        var cmp = key.CompareTo(node.Key);
        return cmp switch
        {
            < 0 => Get(node.Left, key),
            > 0 => Get(node.Right, key),
            _ => node.Value
        };
    }

    public void Put(TKey key, TValue value) =>
        Root = Put(Root, key, value);

    private Node Put(Node node, TKey key, TValue value)
    {
        if (node == null)
        {
            return new Node(key, value, 1);
        }

        var cmp = key.CompareTo(node.Key);
        switch (cmp)
        {
            case < 0:
                node.Left = Put(node.Left, key, value);
                break;
            case > 0:
                node.Right = Put(node.Right, key, value);
                break;
            default:
                node.Value = value;
                break;
        }

        node.SubtreeCount = SizeInner(node.Left) + SizeInner(node.Right) + 1;
        return node;
    }

    public TKey Min() => Min(Root).Key;

    private Node Min(Node node) =>
        node.Left == null ? node : Min(node.Left);

    public TKey Max() => Max(Root).Key;

    private Node Max(Node node) =>
        node.Right == null ? node : Max(node.Right);

    public TKey Floor(TKey key)
    {
        var node = Floor(Root, key);
        return node == null ? default : node.Key;
    }

    private Node Floor(Node node, TKey key)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = key.CompareTo(node.Key);
        switch (cmp)
        {
            case 0:
                return node;
            case < 0:
                return Floor(node.Left, key);
            default:
            {
                var t = Floor(node.Right, key);
                return t ?? node;
            }
        }
    }

    public TKey Select(int k)
    {
        var node = Select(Root, k);
        return node != null ? node.Key : default;
    }

    private Node Select(Node node, int k)
    {
        if (node == null)
        {
            return default;
        }

        var t = SizeInner(node.Left);
        if (t > k)
        {
            return Select(node.Left, k);
        }

        return t < k ? Select(node.Right, k - t - 1) : node;
    }

    public int Rank(TKey key) =>
        Rank(key, Root);

    private int Rank(TKey key, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        var cmp = key.CompareTo(node.Key);
        return cmp switch
        {
            < 0 => Rank(key, node.Left),
            > 0 => 1 + SizeInner(node.Left) + Rank(key, node.Right),
            _ => SizeInner(node.Left)
        };
    }

    public void DeleteMin() =>
        Root = DeleteMin(Root);

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = DeleteMin(node.Left);
        node.SubtreeCount = SizeInner(node.Left) + SizeInner(node.Right) + 1;
        return node;
    }

    public void Delete(TKey key) =>
        Root = Delete(Root, key);

    private Node Delete(Node node, TKey key)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = key.CompareTo(node.Key);
        switch (cmp)
        {
            case < 0:
                node.Left = Delete(node.Left, key);
                break;
            case > 0:
                node.Right = Delete(node.Right, key);
                break;
            default:
            {
                if (node.Right == null)
                {
                    return node.Left;
                }

                if (node.Left == null)
                {
                    return node.Right;
                }

                var t = node;
                node = Min(t.Right);
                node.Right = DeleteMin(t.Right);
                node.Left = t.Left;
                break;
            }
        }

        node.SubtreeCount = SizeInner(node.Left) + SizeInner(node.Right) + 1;
        return node;
    }

    public void Inorder(Action<Node> visit) => Inorder(Root, visit);
    public void Inorder(Node node, Action<Node> visit)
    {
        if (node == null)
        {
            return;
        }

        Inorder(node.Left, visit);
        visit(node);
        Inorder(node.Right, visit);
    }

    public IEnumerable<TKey> Keys() =>
        Keys(Min(), Max());

    private IEnumerable<TKey> Keys(TKey lo, TKey hi)
    {
        Queue<TKey> queue = new();
        Keys(Root, queue, lo, hi);
        return queue;
    }

    private void Keys(Node x, Queue<TKey> queue, TKey lo, TKey hi)
    {
        if (x == null)
        {
            return;
        }

        var cmplo = lo.CompareTo(x.Key);
        var cmphi = hi.CompareTo(x.Key);
        if (cmplo < 0)
        {
            Keys(x.Left, queue, lo, hi);
        }

        if (cmplo <= 0 && cmphi >= 0)
        {
            queue.Enqueue(x.Key);
        }

        if (cmphi > 0)
        {
            Keys(x.Right, queue, lo, hi);
        }
    }

    public class Node
    {
        public Node(TKey key, TValue value, int subtreeCount) =>
            (Key, Value, SubtreeCount) = (key, value, subtreeCount);

        public TKey Key { get; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int SubtreeCount { get; set; }
    }
}