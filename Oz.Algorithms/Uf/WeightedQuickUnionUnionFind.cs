// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.Algorithms.Uf;

public class WeightedQuickUnionUnionFind : UnionFind
{
    private int[] _sizes;

    public WeightedQuickUnionUnionFind(int size) : base(size)
    {
    }

    public WeightedQuickUnionUnionFind()
    {
    }

    public override void Reinitialize(int size)
    {
        base.Reinitialize(size);
        _sizes = new int[size];
        for (var i = 0; i < size; i++)
        {
            _sizes[i] = 1;
        }
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
        var i = Find(p);
        var j = Find(q);
        if (i == j)
        {
            return;
        }

        if (_sizes[i] < _sizes[j])
        {
            _ids[i] = j;
            _sizes[j] += _sizes[i];
        }
        else
        {
            _ids[j] = i;
            _sizes[i] += _sizes[j];
        }

        _count--;
    }
}