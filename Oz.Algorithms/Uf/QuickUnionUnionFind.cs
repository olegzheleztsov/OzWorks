// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.Algorithms.Uf;

public class QuickUnionUnionFind : UnionFind
{
    public QuickUnionUnionFind(int size) : base(size)
    {
    }

    public QuickUnionUnionFind()
    {
    }

    public override int Find(int p)
    {
        while (p != _ids[p])
        {
            p = _ids[p];
        }

        return p;
    }

    public override void Union(int p, int q)
    {
        var pRoot = Find(p);
        var qRoot = Find(q);
        if (pRoot == qRoot)
        {
            return;
        }

        _ids[pRoot] = qRoot;
        _count--;
    }
}