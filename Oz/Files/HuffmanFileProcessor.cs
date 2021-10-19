using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;
using Oz.Algorithms.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oz.Files;

public class HuffmanFileProcessor
{
    private readonly string _fileName;

    public HuffmanFileProcessor(string fileName) =>
        _fileName = fileName;

    public async Task ProcessAsync()
    {
        using var analyzer = new TextFileAnalyzer(_fileName);
        var characterInfos = await analyzer.GetCharacterInfosAsync().ConfigureAwait(false);
        PrintCharacterInfos(characterInfos);

        var huffman = new Huffman(characterInfos.Values.ToList());
        var huffmanTree = huffman.Build();

        var walker = BinaryTreeWalkerFactory.Create(huffmanTree, TreeWalkStrategy.Inorder);
        var nodeCount = 0;
        await walker.WalkAsync(async node =>
        {
            nodeCount++;
            await Task.CompletedTask.ConfigureAwait(false);
        });
        Console.WriteLine($"Huffman tree node's count: {nodeCount}");

        var codeBuilder = new HuffmanCodeBuilder(huffmanTree);
        var codes = await codeBuilder.BuildCodesAsync().ConfigureAwait(false);
        PrintCodes(codes);
    }

    private static void PrintCharacterInfos(Dictionary<char, CharacterInfo> characterInfos)
    {
        foreach (var characterInfo in characterInfos.Values.OrderBy(c => c.Frequency))
        {
            Console.WriteLine($"character: {characterInfo.Character}, frequency: {characterInfo.Frequency}");
        }

        Console.WriteLine($"Total characters: {characterInfos.Count}");
    }

    private static void PrintCodes(Dictionary<char, byte[]> codes)
    {
        foreach (var kvp in codes.Select(p => p).OrderBy(p => p.Value.Length))
        {
            Console.WriteLine($"Character: {kvp.Key}, code: {string.Join(" ", kvp.Value)}");
        }

        Console.WriteLine("---------------------");
    }
}