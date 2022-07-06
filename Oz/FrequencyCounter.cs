// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Oz.Algorithms.Sedgewick.SearchTables;
using System;
using System.IO;

public class FrequencyCounter
{
    public void Run()
    {
        var fileName = @"C:\development\tale.txt";
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"File {fileName} doesn't exist");
            return;
        }

        using var reader = new StreamReader(@"C:\development\tale.txt");
        ArraySt<string, Integer> counterSt = new();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var tokens = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                var loweredToken = token.ToLowerInvariant();
                if (counterSt.Contains(loweredToken))
                {
                    counterSt.Get(loweredToken).Value++;
                }
                else
                {
                    counterSt.Put(loweredToken, new Integer {Value = 1});
                }
            }
        }

        foreach (var key in counterSt.Keys)
        {
            Console.WriteLine($"{key}: {counterSt.Get(key).Value}");
        }

        Console.WriteLine("Deleting...");

        var keys = counterSt.Keys;
        foreach (var key in keys)
        {
            counterSt.Delete(key);
            Console.WriteLine($"Deleted: {key}, size: {counterSt.Size}");
        }

        Console.WriteLine($"Size after deleting: {counterSt.Size}");
    }
}