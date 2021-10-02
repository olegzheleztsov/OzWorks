// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.Algorithms.Uf;

public class QuickFindUnionFind : UnionFind
{
    public QuickFindUnionFind(int size) : base(size){}
    
    public QuickFindUnionFind(){}
    public override int Find(int p) => _ids[p];

    public override void Union(int p, int q)
    {
        var pId = Find(p);
        var qId = Find(q);

        if (pId == qId)
        {
            return;
        }

        for (var i = 0; i < _ids.Length; i++)
        {
            if (_ids[i] == pId)
            {
                _ids[i] = qId;
            }
        }

        _count--;
    }
}