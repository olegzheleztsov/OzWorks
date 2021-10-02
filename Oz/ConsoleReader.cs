// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Exceptions;
using System;
using static System.Console;

namespace Oz;

public class ConsoleReader
{
    private char[] Separators => new[] {' ', '\t'};

    private static void PrintMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            Write($"{message}: ");
        }
    }

    public int ReadInt(string message = "")
    {
        PrintMessage(message);
        var line = ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            throw new InputFinishedException("Empty input is not allowed");
        }
        if (int.TryParse(line, out var number))
        {
            return number;
        }

        throw new ConsoleInputException($"Expected int number, but received: {line}");
    }

    public (int first, int second) ReadIntPair(string message = "")
    {
        PrintMessage(message);
        var line = ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            throw new InputFinishedException("Empty input is not allowed");
        }
        var tokens = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length < 2)
        {
            throw new ConsoleInputException($"Expected to ints, but received: {line}");
        }

        if (!int.TryParse(tokens[0], out var first))
        {
            throw new ConsoleInputException($"Expected int, but received {tokens[0]}");
        }

        if (!int.TryParse(tokens[1], out var second))
        {
            throw new ConsoleInputException($"Expected int, but received {tokens[1]}");
        }

        return (first, second);
    }
}