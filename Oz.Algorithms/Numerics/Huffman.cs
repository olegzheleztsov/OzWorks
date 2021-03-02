using System;
using System.Collections.Generic;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;

namespace Oz.Algorithms.Numerics
{
    public class Huffman
    {
        private readonly IList<CharacterInfo> _characterInfos;

        public Huffman(IList<CharacterInfo> characterInfos)
        {
            _characterInfos = characterInfos;
        }

        public HuffmanTree Build()
        {
            if (_characterInfos.Count < 2)
            {
                throw new ArgumentException("Should be more than 2 characters in the alphabet");
            }

            var priorityQueue = new MaxPriorityQueue<HuffmanNode>();
            foreach (var characterInfo in _characterInfos)
            {
                var node = new HuffmanNode(characterInfo);
                priorityQueue.Insert(node, -((CharacterInfo) node.Value).Frequency);
            }


            for (var i = 0; i < _characterInfos.Count - 1; i++)
            {
                var x = priorityQueue.ExtractMaximum();
                var y = priorityQueue.ExtractMaximum();
                var newFrequency = ((CharacterInfo) x.Value).Frequency + ((CharacterInfo) y.Value).Frequency;
                var newInfo = new CharacterInfo(null, newFrequency);
                var newNode = new HuffmanNode(newInfo);
                newNode.LeftChild = x;
                newNode.RightChild = y;
                x.SetParent(newNode);
                y.SetParent(newNode);
                priorityQueue.Insert(newNode, -newFrequency);
            }

            if (priorityQueue.Length != 1)
            {
                throw new InvalidOperationException(
                    $"Computation error, size of priority queue: {priorityQueue.Length}");
            }

            var tree = new HuffmanTree(info => info.Frequency) {Root = priorityQueue.ExtractMaximum()};
            return tree;
        }
    }

    public class CharacterInfo
    {
        public CharacterInfo(char? character, int frequency)
        {
            Character = character;
            Frequency = frequency;
        }

        public int Frequency { get; private set; }
        public char? Character { get; }

        public bool IsNull => Character == null;

        public void IncrementFrequency(int count) => Frequency += count;
    }

    public class HuffmanNode : ITreeNode
    {
        public HuffmanNode(CharacterInfo characterInfo)
        {
            Value = characterInfo;
        }

        public object Tag { get; set; }

        public ITreeNode LeftChild { get; set; }
        public ITreeNode RightChild { get; set; }
        public ITreeNode ParentNode { get; private set; }

        public void SetParent(ITreeNode parentNode)
        {
            ParentNode = parentNode;
        }

        public object Value { get; }
    }

    public class HuffmanTree : IBinaryTree
    {
        public HuffmanTree(Func<CharacterInfo, int> keySelector)
        {
            KeySelector = obj => keySelector((CharacterInfo) obj);
        }

        public ITreeNode Root { get; set; }

        public bool IsNull(ITreeNode node)
        {
            return node == NullNode;
        }

        public ITreeNode NullNode => null;
        public Func<object, int> KeySelector { get; }

        public bool IsLeaf(ITreeNode node)
        {
            return IsNull(node.LeftChild) && IsNull(node.RightChild);
        }
    }
}