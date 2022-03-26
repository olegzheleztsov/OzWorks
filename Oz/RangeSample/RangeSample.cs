using System;

namespace Oz.RangeSample;

public static class RangeSample
{
    public static void Run()
    {
        string[] words =
        {
            // index from start    index from end
            "The", // 0                   ^9
            "quick", // 1                   ^8
            "brown", // 2                   ^7
            "fox", // 3                   ^6
            "jumped", // 4                   ^5
            "over", // 5                   ^4
            "the", // 6                   ^3
            "lazy", // 7                   ^2
            "dog" // 8                   ^1
        };

        Console.WriteLine($"The last word is {words[^1]}");

        var quickBrownFox = words[1..4];
        foreach (var word in quickBrownFox)
        {
            Console.Write($"< {word} >");
        }

        Console.WriteLine();

        var allWords = words[..]; // contains "The" through "dog".
        var firstPhrase = words[..4]; // contains "The" through "fox"
        var lastPhrase = words[6..]; // contains "the, "lazy" and "dog"
        foreach (var word in allWords)
        {
            Console.Write($"< {word} >");
        }

        Console.WriteLine();
        foreach (var word in firstPhrase)
        {
            Console.Write($"< {word} >");
        }

        Console.WriteLine();
        foreach (var word in lastPhrase)
        {
            Console.Write($"< {word} >");
        }

        Console.WriteLine();
        Console.WriteLine("-----------");
        var the = ^3;
        Console.WriteLine(words[the]);
        var phrase = 1..4;
        var text = words[phrase];
        foreach (var word in text)
        {
            Console.Write($"< {word} >");
        }

        Console.WriteLine();
    }
}