using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;

namespace Oz.Algorithms.Numerics
{
    public class HuffmanCodeBuilder
    {
        private readonly HuffmanTree _huffmanTree;
        private readonly Dictionary<char, byte[]> _codes = new Dictionary<char, byte[]>();

        public HuffmanCodeBuilder(HuffmanTree huffmanTree)
        {
            _huffmanTree = huffmanTree;
        }

        public async Task<Dictionary<char, byte[]>> BuildCodesAsync()
        {
            _codes.Clear();
            var treeWalker = BinaryTreeWalkerFactory.Create(_huffmanTree, TreeWalkStrategy.Preorder);
            await treeWalker.WalkAsync(OnWalkAsync).ConfigureAwait(false);
            return _codes;
        }

        private async Task OnWalkAsync(ITreeNode node)
        {
            var huffmanNode = (HuffmanNode) node;
            if (huffmanNode.ParentNode != null)
            {
                var parentPath = (byte[]) ((HuffmanNode) huffmanNode.ParentNode).Tag;
                var parentPathList = new List<byte>();
                parentPathList.AddRange(parentPath);

                if (huffmanNode.ParentNode.LeftChild == huffmanNode)
                {
                    parentPathList.Add(0);
                }
                else if(huffmanNode.ParentNode.RightChild == huffmanNode)
                {
                    parentPathList.Add(1);
                }

                huffmanNode.Tag = parentPathList.ToArray();
            }
            else
            {
                huffmanNode.Tag = new byte[] { };
            }
            
            FillDictionary(huffmanNode);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        private void FillDictionary(HuffmanNode huffmanNode)
        {
            if (_huffmanTree.IsLeaf(huffmanNode))
            {
                var characterInfo = (CharacterInfo) huffmanNode.Value;
                if (!characterInfo.IsNull)
                {
                    Debug.Assert(characterInfo.Character != null, "characterInfo.Character != null");
                    _codes.Add(characterInfo.Character.Value, (byte[])huffmanNode.Tag);
                }
                else
                {
                    throw new InvalidOperationException($"Leaf must be non null character");
                }
            }
        }
    }
}