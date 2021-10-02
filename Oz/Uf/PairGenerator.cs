// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.Uf;

public class PairGenerator
{
    private readonly int _size;

    public PairGenerator(int size) =>
        _size = size;

    public (int, int) Generate() =>
        (Random.Shared.Next(_size), Random.Shared.Next(_size));

    public List<(int, int)> GetSequence(int length)
    {
        List<(int, int)> pairs = new();
        for (var i = 0; i < length; i++)
        {
            pairs.Add(Generate());
        }

        return pairs;
    }
}