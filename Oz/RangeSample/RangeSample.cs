using System;

namespace Oz.RangeSample
{
    public static class RangeSample
    {
        public static void Run()
        {
            string[] words = new string[]
            {
                // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };     
            
            Console.WriteLine($"The last word is {words[^1]}");

            string[] quickBrownFox = words[1..4];
            foreach (var word in quickBrownFox)
            {
                Console.Write($"< {word} >");
            }
            Console.WriteLine();
            
            string[] allWords = words[..]; // contains "The" through "dog".
            string[] firstPhrase = words[..4]; // contains "The" through "fox"
            string[] lastPhrase = words[6..]; // contains "the, "lazy" and "dog"
            foreach (var word in allWords)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in firstPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in lastPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine();
            Console.WriteLine("-----------");
            Index the = ^3;
            Console.WriteLine(words[the]);
            Range phrase = 1..4;
            string[] text = words[phrase];
            foreach (var word in text)
                Console.Write($"< {word} >");
            Console.WriteLine();
        }
    }
}