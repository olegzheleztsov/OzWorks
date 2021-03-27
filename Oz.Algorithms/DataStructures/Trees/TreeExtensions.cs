using System.Text;

namespace Oz.Algorithms.DataStructures.Trees
{
    public static class TreeExtensions
    {
        public static ITreeNode Successor(this IBinaryTree tree, ITreeNode node)
        {
            if (!tree.IsNull(node.RightChild))
            {
                var minimumSearcher = TreeMinimumSearcherFactory.Create<ITreeNode>(tree, SearchMethod.Recursive);
                return minimumSearcher.Minimum(node.RightChild);
            }

            var parent = node.ParentNode;
            while (parent != null && !tree.IsNull(parent) && node == parent.RightChild)
            {
                node = parent;
                parent = parent.ParentNode;
            }

            return parent;
        }

        public static ITreeNode Predecessor(this IBinaryTree tree, ITreeNode node)
        {
            if (!tree.IsNull(node.LeftChild))
            {
                return TreeMaximumSearcherFactory.Create(tree).Maximum(node.LeftChild);
            }

            var parent = node.ParentNode;
            while (parent != null && !tree.IsNull(parent) && node == parent.LeftChild)
            {
                node = parent;
                parent = parent.ParentNode;
            }

            return parent;
        }
        
        public static void TreeLeftRotate(this IBinaryTree tree, ITreeNode pivotNode)
        {
            var rotateNode = pivotNode.RightChild;
            if (tree.IsNull(rotateNode))
            {
                return;
            }

            pivotNode.RightChild = rotateNode.LeftChild;
            if (!tree.IsNull(rotateNode.LeftChild))
            {
                rotateNode.LeftChild.SetParent(pivotNode);
            }

            rotateNode.SetParent(pivotNode.ParentNode);
            if (tree.IsNull(pivotNode.ParentNode))
            {
                tree.Root = rotateNode;
            }
            else if (pivotNode == pivotNode.ParentNode.LeftChild)
            {
                pivotNode.ParentNode.LeftChild = rotateNode;
            }
            else
            {
                pivotNode.ParentNode.RightChild = rotateNode;
            }

            rotateNode.LeftChild = pivotNode;
            pivotNode.SetParent(rotateNode);
        }

        public static void TreeRightRotate(this IBinaryTree tree, ITreeNode pivotNode)
        {
            var rotatedNode = pivotNode.LeftChild;
            if (tree.IsNull(rotatedNode))
            {
                return;
            }

            pivotNode.LeftChild = rotatedNode.RightChild;
            if (!tree.IsNull(rotatedNode.RightChild))
            {
                rotatedNode.RightChild.SetParent(pivotNode);
            }

            rotatedNode.SetParent(pivotNode.ParentNode);
            if (tree.IsNull(pivotNode.ParentNode))
            {
                tree.Root = rotatedNode;
            }
            else if (pivotNode == pivotNode.ParentNode.LeftChild)
            {
                pivotNode.ParentNode.LeftChild = rotatedNode;
            }
            else
            {
                pivotNode.ParentNode.RightChild = rotatedNode;
            }

            rotatedNode.RightChild = pivotNode;
            pivotNode.SetParent(rotatedNode);
        }

        public static void Insert(this IBinaryTree tree, IColoredTreeNode nodeToInsert)
        {
            var previousNode = tree.NullNode;
            var currentNode = tree.Root;
            while (!tree.IsNull(currentNode))
            {
                previousNode = currentNode;
                currentNode = tree.KeySelector(nodeToInsert.Value) < tree.KeySelector(currentNode.Value)
                    ? currentNode.LeftChild
                    : currentNode.RightChild;
            }

            nodeToInsert.SetParent(previousNode);
            if (tree.IsNull(previousNode))
            {
                tree.Root = nodeToInsert;
            }
            else if (tree.KeySelector(nodeToInsert.Value) < tree.KeySelector(previousNode.Value))
            {
                previousNode.LeftChild = nodeToInsert;
            }
            else
            {
                previousNode.RightChild = nodeToInsert;
            }

            nodeToInsert.LeftChild = tree.NullNode;
            nodeToInsert.RightChild = tree.NullNode;
            nodeToInsert.Color = TreeNodeColor.Red;
            tree._InsertFixup(nodeToInsert);
        }

        public static void Delete(this IBinaryTree tree, IColoredTreeNode nodeToDelete)
        {
            var fixingNode = nodeToDelete;
            var yOriginalColor = fixingNode.Color;
            IColoredTreeNode workingNode;
            if (tree.IsNull(nodeToDelete.LeftChild))
            {
                workingNode = (IColoredTreeNode) nodeToDelete.RightChild;
                tree.Transplant(nodeToDelete, nodeToDelete.RightChild);
            }
            else if (tree.IsNull(nodeToDelete.RightChild))
            {
                workingNode = (IColoredTreeNode) nodeToDelete.LeftChild;
                tree.Transplant(nodeToDelete, nodeToDelete.LeftChild);
            }
            else
            {
                var minSearcher = TreeMinimumSearcherFactory.Create(tree, SearchMethod.Recursive);
                fixingNode = (IColoredTreeNode) minSearcher.Minimum(nodeToDelete.RightChild);
                yOriginalColor = fixingNode.Color;
                workingNode = (IColoredTreeNode) fixingNode.RightChild;
                if (fixingNode.ParentNode == nodeToDelete)
                {
                    workingNode.SetParent(fixingNode);
                }
                else
                {
                    tree.Transplant(fixingNode, fixingNode.RightChild);
                    fixingNode.RightChild = nodeToDelete.RightChild;
                    fixingNode.RightChild.SetParent(fixingNode);
                }

                tree.Transplant(nodeToDelete, fixingNode);
                fixingNode.LeftChild = nodeToDelete.LeftChild;
                fixingNode.LeftChild.SetParent(fixingNode);
                fixingNode.Color = nodeToDelete.Color;
            }

            if (yOriginalColor == TreeNodeColor.Black)
            {
                tree._DeleteFixup(workingNode);
            }
        }

        private static void _InsertFixup(this IBinaryTree tree, IColoredTreeNode nodeToFixup)
        {
            while (((IColoredTreeNode) nodeToFixup.ParentNode).Color == TreeNodeColor.Red)
            {
                if (nodeToFixup.ParentNode == nodeToFixup.ParentNode.ParentNode.LeftChild)
                {
                    var rightUncleNode = nodeToFixup.ParentNode.ParentNode.RightChild;
                    if (((IColoredTreeNode) rightUncleNode).Color == TreeNodeColor.Red)
                    {
                        ((IColoredTreeNode) nodeToFixup.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) rightUncleNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) nodeToFixup.ParentNode.ParentNode).Color = TreeNodeColor.Red;
                        nodeToFixup = (IColoredTreeNode) nodeToFixup.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (nodeToFixup == nodeToFixup.ParentNode.RightChild)
                        {
                            nodeToFixup = (IColoredTreeNode) nodeToFixup.ParentNode;
                            tree.TreeLeftRotate(nodeToFixup);
                        }

                        ((IColoredTreeNode) nodeToFixup.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) nodeToFixup.ParentNode.ParentNode).Color = TreeNodeColor.Red;
                        tree.TreeRightRotate(nodeToFixup.ParentNode.ParentNode);
                    }
                }
                else
                {
                    var leftUncleNode = nodeToFixup.ParentNode.ParentNode.LeftChild;
                    if (((IColoredTreeNode) leftUncleNode).Color == TreeNodeColor.Red)
                    {
                        ((IColoredTreeNode) nodeToFixup.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) leftUncleNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) nodeToFixup.ParentNode.ParentNode).Color = TreeNodeColor.Red;
                        nodeToFixup = (IColoredTreeNode) nodeToFixup.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (nodeToFixup == nodeToFixup.ParentNode.LeftChild)
                        {
                            nodeToFixup = (IColoredTreeNode) nodeToFixup.ParentNode;
                            tree.TreeRightRotate(nodeToFixup);
                        }

                        ((IColoredTreeNode) nodeToFixup.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) nodeToFixup.ParentNode.ParentNode).Color = TreeNodeColor.Red;
                        tree.TreeLeftRotate(nodeToFixup.ParentNode.ParentNode);
                    }
                }
            }

            ((IColoredTreeNode) tree.Root).Color = TreeNodeColor.Black;
        }

        private static void _DeleteFixup(this IBinaryTree tree, IColoredTreeNode fixupNode)
        {
            while (fixupNode != tree.Root && fixupNode.Color == TreeNodeColor.Black)
            {
                if (fixupNode == fixupNode.ParentNode.LeftChild)
                {
                    var workingNode = fixupNode.ParentNode.RightChild;
                    if (((IColoredTreeNode) workingNode).Color == TreeNodeColor.Red)
                    {
                        ((IColoredTreeNode) workingNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) fixupNode.ParentNode).Color = TreeNodeColor.Red;
                        tree.TreeLeftRotate(fixupNode.ParentNode);
                        workingNode = fixupNode.ParentNode.RightChild;
                    }

                    if (((IColoredTreeNode) workingNode.LeftChild).Color == TreeNodeColor.Black &&
                        ((IColoredTreeNode) workingNode.RightChild).Color == TreeNodeColor.Black)
                    {
                        fixupNode.Color = TreeNodeColor.Red;
                        fixupNode = (IColoredTreeNode) fixupNode.ParentNode;
                    }
                    else
                    {
                        if (((IColoredTreeNode) workingNode.RightChild).Color == TreeNodeColor.Black)
                        {
                            ((IColoredTreeNode) workingNode.LeftChild).Color = TreeNodeColor.Black;
                            ((IColoredTreeNode) workingNode).Color = TreeNodeColor.Red;
                            tree.TreeRightRotate(workingNode);
                            workingNode = fixupNode.ParentNode.RightChild;
                        }

                        ((IColoredTreeNode) workingNode).Color = ((IColoredTreeNode) fixupNode.ParentNode).Color;
                        ((IColoredTreeNode) fixupNode.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) workingNode.RightChild).Color = TreeNodeColor.Black;
                        tree.TreeLeftRotate(fixupNode.ParentNode);
                        fixupNode = (IColoredTreeNode) tree.Root;
                    }
                }
                else
                {
                    var workingNode = (IColoredTreeNode) fixupNode.ParentNode.LeftChild;
                    if (workingNode.Color == TreeNodeColor.Red)
                    {
                        workingNode.Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) fixupNode.ParentNode).Color = TreeNodeColor.Red;
                        tree.TreeRightRotate(fixupNode.ParentNode);
                        workingNode = (IColoredTreeNode) fixupNode.ParentNode.LeftChild;
                    }

                    if (((IColoredTreeNode) workingNode.RightChild).Color == TreeNodeColor.Black &&
                        ((IColoredTreeNode) workingNode.LeftChild).Color == TreeNodeColor.Black)
                    {
                        fixupNode.Color = TreeNodeColor.Red;
                        fixupNode = (IColoredTreeNode) fixupNode.ParentNode;
                    }
                    else
                    {
                        if (((IColoredTreeNode) workingNode.LeftChild).Color == TreeNodeColor.Black)
                        {
                            ((IColoredTreeNode) workingNode.RightChild).Color = TreeNodeColor.Black;
                            workingNode.Color = TreeNodeColor.Red;
                            tree.TreeLeftRotate(workingNode);
                            workingNode = (IColoredTreeNode) fixupNode.ParentNode.LeftChild;
                        }

                        workingNode.Color = ((IColoredTreeNode) fixupNode.ParentNode).Color;
                        ((IColoredTreeNode) fixupNode.ParentNode).Color = TreeNodeColor.Black;
                        ((IColoredTreeNode) workingNode.LeftChild).Color = TreeNodeColor.Black;
                        tree.TreeRightRotate(fixupNode.ParentNode);
                        fixupNode = (IColoredTreeNode) tree.Root;
                    }
                }
            }

            fixupNode.Color = TreeNodeColor.Black;
        }

        private static void Transplant(this IBinaryTree tree, ITreeNode firstNode, ITreeNode secondNode)
        {
            if (tree.IsNull(firstNode.ParentNode))
            {
                tree.Root = secondNode;
            }
            else if (firstNode == firstNode.ParentNode.LeftChild)
            {
                firstNode.ParentNode.LeftChild = secondNode;
            }
            else
            {
                firstNode.ParentNode.RightChild = secondNode;
            }

            secondNode.SetParent(firstNode.ParentNode);
        }

        public static string GetColoredTreeString(this IBinaryTree tree)
        {
            var stringBuilder = new StringBuilder();
            var preorderWalker =
                BinaryTreeWalkerFactory.Create(tree, TreeWalkStrategy.Preorder);
            preorderWalker.Walk(node =>
            {
                var coloredNode = node as IColoredTreeNode;
                string nodeStr = string.Empty;
                if (tree.IsNull(coloredNode.ParentNode))
                {
                    nodeStr = "NIL => ";
                }
                else
                {
                    nodeStr =
                        $"{coloredNode.ParentNode.Value.ToString()}{GetColorString(((IColoredTreeNode) coloredNode.ParentNode).Color)} => ";
                }

                nodeStr += $"{coloredNode.Value.ToString()}{GetColorString(coloredNode.Color)}  ";

                if (node is IOrderStatTreeNode)
                {
                    nodeStr += $"Size: {(node as IOrderStatTreeNode).Size}  ";
                }

                if (tree.IsNull(coloredNode.LeftChild))
                {
                    nodeStr += "Left: NIL  ";
                }
                else
                {
                    nodeStr +=
                        $"Left: {coloredNode.LeftChild.Value.ToString()}{GetColorString(((IColoredTreeNode) coloredNode.LeftChild).Color)}  ";
                }

                if (tree.IsNull(coloredNode.RightChild))
                {
                    nodeStr += "Right: NIL  ";
                }
                else
                {
                    nodeStr +=
                        $"Right: {coloredNode.RightChild.Value.ToString()}{GetColorString(((IColoredTreeNode) coloredNode.RightChild).Color)}  ";
                }

                stringBuilder.AppendLine(nodeStr);
            });
            return stringBuilder.ToString();
        }

        private static string GetColorString(TreeNodeColor color) => color == TreeNodeColor.Black ? "B" : "R";
    }
}