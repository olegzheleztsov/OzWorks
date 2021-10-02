// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Newtonsoft.Json;

namespace Oz.Algorithms.Uf;

public abstract class UnionFind
{
    protected int _count;
    protected int[] _ids;

    public UnionFind(int n) =>
        Reinitialize(n);

    public UnionFind()
    {
    }

    public int Count => _count;

    public virtual void Reinitialize(int size)
    {
        _count = size;
        _ids = new int[size];
        for (var i = 0; i < size; i++)
        {
            _ids[i] = i;
        }
    }

    public bool IsConnected(int p, int q) => Find(p) == Find(q);

    public abstract int Find(int p);
    public abstract void Union(int p, int q);

    public override string ToString() =>
        JsonConvert.SerializeObject(new {Count = _count, Ids = _ids});
}