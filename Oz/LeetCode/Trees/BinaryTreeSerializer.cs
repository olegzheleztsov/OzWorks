using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using static System.String;

namespace Oz.LeetCode.Trees
{
    public class BinaryTreeSerializer
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root == null)
            {
                return "[]";
            }
            
            var depth = Depth(root);
            var serializedArray = new int?[(int)Math.Pow(2, depth + 1) - 1];
            SerializeAtIndex(root, 0, serializedArray);
            return SerializeArray(serializedArray);

            static string SerializeArray(int?[] values)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append('[');
                var index = values.Length - 1;
                while (index >= 0 && values[index] == null)
                {
                    index--;
                }

                if (index < 0)
                {
                    return "[]";
                }

                for (var i = 0; i <= index; i++)
                {
                    stringBuilder.Append(values[i]?.ToString() ?? "null");
                    stringBuilder.Append(',');   
                }

                if (stringBuilder[^1] == ',')
                {
                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }

                stringBuilder.Append(']');
                return stringBuilder.ToString();
            }

            static void SerializeAtIndex(TreeNode node, int index, IList<int?> values)
            {
                if (index < values.Count)
                {
                    values[index] = node.val;
                }

                if (node.left != null)
                {
                    SerializeAtIndex(node.left, 2 * index + 1, values);
                }

                if (node.right != null)
                {
                    SerializeAtIndex(node.right, 2 * index + 2, values);
                }
            }
        }

        public TreeNode deserialize(string data)
        {
            data = data[1..^1];
            string[] tokens = data.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return CreateTree(tokens, 0);

            TreeNode CreateTree(string[] tokens, int index)
            {
                if (index >= tokens.Length)
                {
                    return null;
                }

                if (!int.TryParse(tokens[index], out var val))
                {
                    return null;
                }

                var node = new TreeNode(val)
                {
                    left = CreateTree(tokens, 2 * index + 1), 
                    right = CreateTree(tokens, 2 * index + 2)
                };
                return node;
            }
        }
        private int Depth(TreeNode root)
        {
            var count = 0;
            VisitOnDepth(root, 0, (depth) =>
            {
                if (depth > count)
                {
                    count = depth;
                }
            });
            return count;
            
            void VisitOnDepth(TreeNode node, int depth, Action<int> action)
            {
                if (node != null)
                {
                    action(depth);
                    if (node.left != null)
                    {
                        VisitOnDepth(node.left, depth + 1, action);
                    }

                    if (node.right != null)
                    {
                        VisitOnDepth(node.right, depth + 1, action);
                    }
                }
                
            }
        }

        // // Decodes your encoded data to tree.


        private TreeNode CreateTestTree()
        {
            var rootValue = 1;
            var (root, n3) = rootValue.CreateTreeNode().WithLeft(2).parent.WithRight(3);
            n3.WithLeft(4).parent.WithRight(5);
            return root;
        }

        public void TestSerialization()
        {
            var tree = CreateTestTree();
            var serialized = serialize(tree);
            WriteLine(serialized);
            var newTree = deserialize(serialized);
            
        }
    }
}