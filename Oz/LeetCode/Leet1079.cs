// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.LeetCode;

public class Leet1079
{
    public int NumTilePossibilities(string tiles)
    {
        var sequences = new HashSet<string>();
        var list = tiles.ToList();

        for (var i = 1; i <= tiles.Length; i++)
        {
            NumTilePossibilitiesHelper(new StringBuilder(), sequences, list, i);
        }

        return sequences.Count;
    }

    private void NumTilePossibilitiesHelper(StringBuilder currentString, HashSet<string> sequences, List<char> tiles,
        int length)
    {
        if (currentString.Length == length)
        {
            sequences.Add(currentString.ToString());
            return;
        }

        var currStringLength = currentString.Length;

        for (var i = 0; i < tiles.Count; i++)
        {
            var ch = tiles[i];
            currentString.Append(ch);
            tiles.RemoveAt(i);
            NumTilePossibilitiesHelper(currentString, sequences, tiles, length);
            tiles.Insert(i, ch);
            currentString.Remove(currStringLength, currentString.Length - currStringLength);
        }
    }
}