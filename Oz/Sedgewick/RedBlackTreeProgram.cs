// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Sedgewick.SearchTables;
using System;

namespace Oz.Sedgewick;

public class RedBlackTreeProgram
{
    public static void Run()
    {
        RedBlackTree<int, int> tree = new();

        for (var i = 0; i < 10; i++)
        {
            tree.Put(i, i);
        }

        tree.Inorder(Console.WriteLine);
    }
}