using System;
using System.Collections.Generic;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;
using Oz.Algorithms.Numerics;

namespace Oz
{
    public class HuffmanCase
    {
        public void Run()
        {
            List<CharacterInfo> characterInfos = new List<CharacterInfo>()
            {
                new CharacterInfo('a', 1),
                new CharacterInfo('b', 1),
                new CharacterInfo('c', 2),
                new CharacterInfo('d', 3),
                new CharacterInfo('e', 5),
                new CharacterInfo('f', 8),
                new CharacterInfo('g', 13),
                new CharacterInfo('h', 21)
            };
            Huffman huffman = new Huffman(characterInfos);
            var tree = huffman.Build();
            var binaryTreeWalker = BinaryTreeWalkerFactory.Create(tree, TreeWalkStrategy.Preorder);
            binaryTreeWalker.Walk(node =>
            {
                HuffmanNode huffmanNode = node as HuffmanNode;
                
                if (huffmanNode.ParentNode != null)
                {
                    var parentPath = (huffmanNode.ParentNode as HuffmanNode).Tag as byte[];
                    List<byte> parentPathList = new List<byte>();
                    parentPathList.AddRange(parentPath);
                    
                    if (huffmanNode.ParentNode.LeftChild == huffmanNode)
                    {
                        parentPathList.Add(0);
                        huffmanNode.Tag = parentPathList.ToArray();
                    } else if (huffmanNode.ParentNode.RightChild == huffmanNode)
                    {
                        parentPathList.Add(1);
                        huffmanNode.Tag = parentPathList.ToArray();
                    }
                }
                else
                {
                    huffmanNode.Tag = new byte[] { };
                }

                if (tree.IsLeaf(huffmanNode))
                {
                    var charInfo = huffmanNode.Value as CharacterInfo;
                    if (!charInfo.IsNull)
                    {
                        Console.WriteLine($"{charInfo.Character}: Freq: {charInfo.Frequency}: {string.Join(" ", huffmanNode.Tag as byte[])}");
                        
                    }
                    else
                    {
                        throw new InvalidOperationException($"Leaf must be non null character");
                    }
                }
                else
                {
                    Console.WriteLine($"Inner node freq: {(huffmanNode.Value as CharacterInfo).Frequency}: {string.Join(" ", huffmanNode.Tag as byte[])}");
                }
                
            });
        }
    }
}