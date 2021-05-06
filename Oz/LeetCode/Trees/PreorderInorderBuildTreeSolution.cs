using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static System.Console;
using static System.String;

namespace Oz.LeetCode.Trees
{
    public class PreorderInorderBuildTreeSolution
    {
        public TreeNode BuildTree(int[] preorder, int[] inorder) {
            
            if (preorder?.Length == 0 || inorder?.Length == 0)
            {
                return null;
            }

            return Solve(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
            
            TreeNode Solve(IReadOnlyList<int> preorderArray, int preorderStart, int preorderEnd, IReadOnlyList<int> inorderArray, int inorderStart,
                int inorderEnd)
            {
                if (preorderStart > preorderEnd || inorderStart > inorderEnd)
                {
                    return null;
                }
                
                var rootValue = preorderArray[preorderStart];
                var rootNode = new TreeNode(rootValue);
                var inorderIndex = -1;
                for (var i = inorderStart; i <= inorderEnd; i++)
                {
                    if (inorderArray[i] == rootValue)
                    {
                        inorderIndex = i;
                        break;
                    }
                }

                if (inorderIndex < 0)
                {
                    throw new InvalidOperationException(nameof(inorderIndex));
                }

                rootNode.left = Solve(
                    preorderArray,
                    preorderStart + 1,
                    preorderStart + (inorderIndex - inorderStart),
                    inorder, inorderStart, inorderIndex - 1);
                rootNode.right = Solve(
                    preorderArray,
                    preorderStart + (inorderIndex - inorderStart) + 1,
                    preorderEnd,
                    inorderArray,
                    inorderIndex + 1,
                    inorderEnd
                );
                return rootNode;
            }
        }

        public void Test()
        {
            int[] preorder = {3, 9, 20, 15, 7};
            int[] inorder = {9, 3, 15, 20, 7};
            var root = BuildTree(preorder, inorder);

            var treeSolutions = new TreeSolutions();
            var testResultPreorder = TreeSolutions.PreorderTraversal(root);
            WriteLine($"Preorder: {Join(", ", testResultPreorder)}");
            var testResultInorder = treeSolutions.InorderTraversal(root);
            WriteLine($"inorder: {Join(", ", testResultInorder)}");
        }
    }
}