﻿#region

using Oz.Algorithms;
using Oz.Algorithms.DataStructures.Trees;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Strings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using static System.Console;

// ReSharper disable UnusedMember.Local

#endregion

namespace Oz.Misc;

public class CormenMisc
{
    private static void TestSuccessorsInRbTree()
    {
        var tree = new RbTree<NodeData>(data => data.Value);
        var rndIntegers = Enumerable.Range(1, 10).ToArray().Shuffled();

        foreach (var val in rndIntegers)
        {
            tree.Insert(tree.CreateNode(new NodeData(val)));
        }

        var searcher = tree.CreateSearcher();
        var node1 = searcher.Search(1);
        while (!tree.IsNull(node1))
        {
            if (node1 != null)
            {
                WriteLine(node1.Data.ToString());
                node1 = tree.Successor(node1) as RbTreeNode<NodeData>;
            }
        }

        WriteLine("--------------------------------");

        var n10 = searcher.Search(10);
        while (!tree.IsNull(n10))
        {
            if (n10 != null)
            {
                WriteLine(n10.Data.ToString());
                n10 = tree.Predecessor(n10) as RbTreeNode<NodeData>;
            }
        }

        WriteLine("---------------------");
        WriteLine($"Max: {tree.CreateMaximumSearcher().Maximum(tree.Root as RbTreeNode<NodeData>).Data}");
        WriteLine($"Min: {tree.CreateMinimumSearcher().Minimum().Data}");
    }

    private static void TestMatchKnuthMorrisPratt(string fileName, string pattern)
    {
        var results = StringWrapperUtils.MatchKnuthMorrisPratt(File.ReadAllText(fileName), pattern);
        WriteLine("KNUTH-MORRIS-PRATH:");
        WriteLine($"Count of matches: {results.Count}");
        WriteLine($"Matching: {string.Join(", ", results)}");
    }

    private static void TestMatchFiniteAutomation(string fileName, string pattern)
    {
        // string source = "aaaabbbababababaaabbdbdb";
        // var alphabet = source.Select(ch => ch).Distinct().ToList();
        // var resultPositions = StringWrapperUtils.MatchFiniteAutomation(source, "aab", alphabet);
        // Console.WriteLine($"Matches: {string.Join(" ", resultPositions)}");

        var text = File.ReadAllText(fileName);

        var chars = new HashSet<char>();
        foreach (var ch in text)
        {
            chars.Add(ch);
        }

        var resultPositions = StringWrapperUtils.MatchFiniteAutomation(text, pattern, chars);

        WriteLine("FINITE-AUTOMATION");
        WriteLine($"Count of matches: {resultPositions.Count}");
        WriteLine($"Matching: {string.Join(", ", resultPositions)}");
    }


    private static void TestMatchRabinKarp(string fileName, string pattern)
    {
        //read text
        var text = File.ReadAllText(fileName);

        //find unique characers
        var chars = new HashSet<char>();
        foreach (var ch in text)
        {
            chars.Add(ch);
        }

        WriteLine($"Different chars: {chars.Count}");

        //create mapping chars -> codes
        var counter = 0;
        var mapping = chars.OrderBy(c => c)
            .ToDictionary(ch => ch, _ => counter++);

        //find prime number for algorithm
        var baseValue = mapping.Count;
        var primeHigh = int.MaxValue / baseValue;
        var targetPrime = 0;
        while (primeHigh >= 3)
        {
            //only odd numbers allowed
            if (primeHigh % 2 != 0)
            {
                var pseudoRandom = new MillerRabinPseudorandom(primeHigh);
                if (pseudoRandom.MayBePrime(1000))
                {
                    targetPrime = primeHigh;
                    break;
                }
            }

            primeHigh--;
        }

        if (targetPrime == 0)
        {
            WriteLine("Not found prime");
            return;
        }

        WriteLine($"Target prime found: {targetPrime}");

        //find matching
        var results = StringWrapperUtils.MatchRabinKarp(text, pattern, targetPrime, mapping);

        WriteLine("RABIN-KARP:");
        WriteLine($"Count of matches: {results.Count}");
        WriteLine($"Matching: {string.Join(", ", results)}");

        // foreach (var index in results)
        // {
        //     Console.WriteLine($"{index}: {new StringWrapper(text).Substring(index, pattern.Length)}");
        // }
    }

    private static void TestMillerRabinPrime()
    {
        for (var i = 3; i < 100; i += 2)
        {
            var pseudorandom = new MillerRabinPseudorandom(i);
            WriteLine($"{i} is prime: {pseudorandom.MayBePrime(100)}");
        }
    }

    private static void TestRandomBigIntegers()
    {
        var source = new DefaultRandomSource();
        int zeroCount = 0, thousandCount = 0;
        for (var i = 0; i < 10000; i++)
        {
            var val = source.RandomBigInteger(500, 1000);

            WriteLine($"Rand in [0, 1000] is {val}");
            if (val == 500)
            {
                zeroCount++;
            }

            if (val == 1000)
            {
                thousandCount++;
            }
        }

        WriteLine($"Zero count: {zeroCount}, Thousand count: {thousandCount}");
    }

    private static void TestPrimes()
    {
        BigInteger value = 2;

        for (; value < new BigInteger(10000000); value++)
        {
            var pseudoprime = new Pseudoprime(value);
            if (pseudoprime.MayBePrime)
            {
                WriteLine($"{value} may be prime");
            }
        }
    }

    private static void TestBigIntegerAsBinaryString()
    {
        BigInteger v1 = 3;
        WriteLine(v1.ToBinaryString());

        BigInteger v2 = 100;
        WriteLine(v2.ToString());

        BigInteger v3 = 1454456;
        WriteLine(v3.ToBinaryString());
    }

    private static void TestBigIntegersDivision()
    {
        BigInteger minus5 = -5;
        BigInteger three = 3;
        var floor = BigInteger.DivRem(minus5, three, out var remainder);
        WriteLine($"Floor: {floor}, Remainder: {remainder}");
    }

    private static void TestFloors()
    {
        double a = 5, b = 3;
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        a = 6;
        b = 3;
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (7, 3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (0, 3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (0, -3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (-1, 3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (-1, -3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (1, -3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (-5, 3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (5, -3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");

        (a, b) = (-5, -3);
        WriteLine($"floor {a} / {b} = {Math.Floor(a / b)}");
    }

    public record NodeData(int Value);
}