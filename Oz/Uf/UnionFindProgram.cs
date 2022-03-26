// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Uf;
using Oz.Exceptions;
using System;
using static System.Console;

namespace Oz.Uf;

public class UnionFindProgram
{
    private readonly UnionFind _unionFind;

    public UnionFindProgram(UnionFind unionFind) =>
        _unionFind = unionFind;

    public void Run()
    {
        var consoleReader = new ConsoleReader();
        var inputFinished = false;

        var unionSize = 0;
        try
        {
            unionSize = consoleReader.ReadInt("Enter union size");
            _unionFind.Reinitialize(unionSize);
        }
        catch (Exception exception)
        {
            WriteLine(exception.Message);
            WriteLine("Exit...");
            inputFinished = true;
        }

        while (!inputFinished)
        {
            try
            {
                var (first, second) = consoleReader.ReadIntPair("Enter int pair");
                if (_unionFind.IsConnected(first, second))
                {
                    WriteLine($"{first} and {second} already connected");
                    continue;
                }

                _unionFind.Union(first, second);
                WriteLine($"{first} was connected to {second}");
            }
            catch (InputFinishedException exception)
            {
                WriteLine("Exit...");
                inputFinished = true;
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
                WriteLine("Continue input...");
            }
        }
    }
}