// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.Algorithms.Uf;

public class QuickUnionWithPathCompressionUnionFind : QuickUnionUnionFind
{
    public QuickUnionWithPathCompressionUnionFind(int size) : base(size) {}
    
    public QuickUnionWithPathCompressionUnionFind() {}

    public override void Union(int p, int q)
    {
        var pRoot = Find(p);
        var qRoot = Find(q);
        if (pRoot == qRoot)
        {
            return;
        }
        _ids[pRoot] = qRoot;

        int pLink = p;
        while (_ids[pLink] != qRoot)
        {
            var nextLink = _ids[pLink];
            _ids[pLink] = qRoot;
            pLink = nextLink;
        }
    }
}