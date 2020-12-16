using System;
using System.Text;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class RbTree<T> : IBinaryTree
    {
        private readonly Func<T, int> _keySelector;
        private RbTreeNode<T> _root;

        public RbTree(Func<T, int> keySelector)
            : this(null, keySelector)
        {
        }

        public RbTree(RbTreeNode<T> root, Func<T, int> keySelector)
        {
            _root = root;
            _keySelector = keySelector;
            Nil = new RbTreeNode<T>(default);
            Nil.RbLeft = Nil.RbRight = Nil;
            if (_root != null)
            {
                _root.RbParent = Nil;
            }
            else
            {
                _root = Nil;
            }
        }

        private RbTreeNode<T> Nil { get; }

        public ITreeNode Root => _root;


        public bool IsNull(ITreeNode node)
        {
            return node == Nil;
        }

        public virtual void Insert(RbTreeNode<T> nodeToInsert)
        {
            var previousNode = Nil;
            var currentNode = _root;
            while (!IsNull(currentNode))
            {
                previousNode = currentNode;
                currentNode = _keySelector(nodeToInsert.Data) < _keySelector(currentNode.Data)
                    ? currentNode.RbLeft
                    : currentNode.RbRight;
            }

            nodeToInsert.RbParent = previousNode;
            if (IsNull(previousNode))
            {
                _root = nodeToInsert;
            }
            else if (_keySelector(nodeToInsert.Data) < _keySelector(previousNode.Data))
            {
                previousNode.RbLeft = nodeToInsert;
            }
            else
            {
                previousNode.RbRight = nodeToInsert;
            }

            nodeToInsert.RbLeft = Nil;
            nodeToInsert.RbRight = Nil;
            nodeToInsert.Color = TreeNodeColor.Red;
            _InsertFixup(nodeToInsert);
        }

        public virtual void Delete(RbTreeNode<T> nodeToDelete)
        {
            var fixingNode = nodeToDelete;
            var yOriginalColor = fixingNode.Color;
            RbTreeNode<T> workingNode;
            if (IsNull(nodeToDelete.RbLeft))
            {
                workingNode = nodeToDelete.RbRight;
                Transplant(nodeToDelete, nodeToDelete.RbRight);
            }
            else if (IsNull(nodeToDelete.RbRight))
            {
                workingNode = nodeToDelete.RbLeft;
                Transplant(nodeToDelete, nodeToDelete.RbLeft);
            }
            else
            {
                var minSearcher = TreeMinimumSearcherFactory.Create<RbTreeNode<T>>(this, SearchMethod.Iterative);
                fixingNode = minSearcher.Minimum(nodeToDelete.RbRight);
                yOriginalColor = fixingNode.Color;
                workingNode = fixingNode.RbRight;
                if (fixingNode.RbParent == nodeToDelete)
                {
                    workingNode.RbParent = fixingNode;
                }
                else
                {
                    Transplant(fixingNode, fixingNode.RbRight);
                    fixingNode.RbRight = nodeToDelete.RbRight;
                    fixingNode.RbRight.RbParent = fixingNode;
                }

                Transplant(nodeToDelete, fixingNode);
                fixingNode.RbLeft = nodeToDelete.RbLeft;
                fixingNode.RbLeft.RbParent = fixingNode;
                fixingNode.Color = nodeToDelete.Color;
            }

            if (yOriginalColor == TreeNodeColor.Black)
            {
                DeleteFixup(workingNode);
            }
        }


        public virtual RbTreeNode<T> CreateNode(T data, TreeNodeColor color = TreeNodeColor.Black)
        {
            var node = new RbTreeNode<T>(data, color);
            node.RbLeft = node.RbRight = Nil;
            return node;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var walker = BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Preorder);
            walker.Walk(node => stringBuilder.Append($"{GetNodeRepresentation(node as RbTreeNode<T>)}, "));
            return stringBuilder.ToString();
        }

        public void SetRoot(RbTreeNode<T> root)
        {
            _root = root;
            _root.RbParent = Nil;
        }

        protected virtual string GetNodeRepresentation(RbTreeNode<T> node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            stringBuilder.Append(
                $"Val: {node.Data.ToString()}{GetColorRepresentation(node.Color)}, L: {(IsNull(node.RbLeft) ? "NIL" : node.RbLeft.Data.ToString())}, R: {(IsNull(node.RbRight) ? "NIL" : node.RbRight.Data.ToString())}");
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        private void LeftRotate(RbTreeNode<T> pivotNode)
        {
            var rotatedNode = pivotNode.RbRight;
            if (IsNull(rotatedNode))
            {
                return;
            }

            pivotNode.RbRight = rotatedNode.RbLeft;
            if (!IsNull(rotatedNode.RbLeft))
            {
                rotatedNode.RbLeft.RbParent = pivotNode;
            }

            rotatedNode.RbParent = pivotNode.RbParent;
            if (IsNull(pivotNode.RbParent))
            {
                _root = rotatedNode;
            }
            else if (pivotNode == pivotNode.RbParent.RbLeft)
            {
                pivotNode.RbParent.RbLeft = rotatedNode;
            }
            else
            {
                pivotNode.RbParent.RbRight = rotatedNode;
            }

            rotatedNode.RbLeft = pivotNode;
            pivotNode.RbParent = rotatedNode;
        }

        private void RightRotate(RbTreeNode<T> pivotNode)
        {
            var rotatedNode = pivotNode.RbLeft;
            if (IsNull(rotatedNode))
            {
                return;
            }

            pivotNode.RbLeft = rotatedNode.RbRight;
            if (!IsNull(rotatedNode.RbRight))
            {
                rotatedNode.RbRight.Parent = pivotNode;
            }

            rotatedNode.RbParent = pivotNode.RbParent;
            if (IsNull(pivotNode.RbParent))
            {
                _root = rotatedNode;
            }
            else if (pivotNode == pivotNode.RbParent.RbLeft)
            {
                pivotNode.RbParent.RbLeft = rotatedNode;
            }
            else
            {
                pivotNode.RbParent.RbRight = rotatedNode;
            }

            rotatedNode.RbRight = pivotNode;
            pivotNode.RbParent = rotatedNode;
        }

        private static string GetColorRepresentation(TreeNodeColor color)
        {
            return color == TreeNodeColor.Black ? "B" : "R";
        }

        private void _InsertFixup(RbTreeNode<T> nodeToFixup)
        {
            while (nodeToFixup.RbParent.Color == TreeNodeColor.Red)
            {
                if (nodeToFixup.RbParent == nodeToFixup.RbParent.RbParent.Left)
                {
                    var rightUncleNode = nodeToFixup.RbParent.RbParent.RbRight;
                    if (rightUncleNode.Color == TreeNodeColor.Red)
                    {
                        nodeToFixup.RbParent.Color = TreeNodeColor.Black;
                        rightUncleNode.Color = TreeNodeColor.Black;
                        nodeToFixup.RbParent.RbParent.Color = TreeNodeColor.Red;
                        nodeToFixup = nodeToFixup.RbParent.RbParent;
                    }
                    else
                    {
                        if (nodeToFixup == nodeToFixup.RbParent.RbRight)
                        {
                            nodeToFixup = nodeToFixup.RbParent;
                            LeftRotate(nodeToFixup);
                        }

                        nodeToFixup.RbParent.Color = TreeNodeColor.Black;
                        nodeToFixup.RbParent.RbParent.Color = TreeNodeColor.Red;
                        RightRotate(nodeToFixup.RbParent.RbParent);
                    }
                }
                else
                {
                    var leftUncleNode = nodeToFixup.RbParent.RbParent.RbLeft;
                    if (leftUncleNode.Color == TreeNodeColor.Red)
                    {
                        nodeToFixup.RbParent.Color = TreeNodeColor.Black;
                        leftUncleNode.Color = TreeNodeColor.Black;
                        nodeToFixup.RbParent.RbParent.Color = TreeNodeColor.Red;
                        nodeToFixup = nodeToFixup.RbParent.RbParent;
                    }
                    else
                    {
                        if (nodeToFixup == nodeToFixup.RbParent.RbLeft)
                        {
                            nodeToFixup = nodeToFixup.RbParent;
                            RightRotate(nodeToFixup);
                        }

                        nodeToFixup.RbParent.Color = TreeNodeColor.Black;
                        nodeToFixup.RbParent.RbParent.Color = TreeNodeColor.Red;
                        LeftRotate(nodeToFixup.RbParent.RbParent);
                    }
                }
            }

            _root.Color = TreeNodeColor.Black;
        }

        private void Transplant(RbTreeNode<T> firstNode, RbTreeNode<T> secondNode)
        {
            if (IsNull(firstNode.RbParent))
            {
                _root = secondNode;
            }
            else if (firstNode == firstNode.RbParent.RbLeft)
            {
                firstNode.RbParent.RbLeft = secondNode;
            }
            else
            {
                firstNode.RbParent.RbRight = secondNode;
            }

            secondNode.RbParent = firstNode.RbParent;
        }

        private void DeleteFixup(RbTreeNode<T> fixupNode)
        {
            while (fixupNode != Root && fixupNode.Color == TreeNodeColor.Black)
            {
                if (fixupNode == fixupNode.RbParent.RbLeft)
                {
                    var workingNode = fixupNode.RbParent.RbRight;
                    if (workingNode.Color == TreeNodeColor.Red)
                    {
                        workingNode.Color = TreeNodeColor.Black;
                        fixupNode.RbParent.Color = TreeNodeColor.Red;
                        LeftRotate(fixupNode.RbParent);
                        workingNode = fixupNode.RbParent.RbRight;
                    }

                    if (workingNode.RbLeft.Color == TreeNodeColor.Black &&
                        workingNode.RbRight.Color == TreeNodeColor.Black)
                    {
                        fixupNode.Color = TreeNodeColor.Red;
                        fixupNode = fixupNode.RbParent;
                    }
                    else
                    {
                        if (workingNode.RbRight.Color == TreeNodeColor.Black)
                        {
                            workingNode.RbLeft.Color = TreeNodeColor.Black;
                            workingNode.Color = TreeNodeColor.Red;
                            RightRotate(workingNode);
                            workingNode = fixupNode.RbParent.RbRight;
                        }

                        workingNode.Color = fixupNode.RbParent.Color;
                        fixupNode.RbParent.Color = TreeNodeColor.Black;
                        workingNode.RbRight.Color = TreeNodeColor.Black;
                        LeftRotate(fixupNode.RbParent);
                        fixupNode = _root;
                    }
                }
                else
                {
                    var workingNode = fixupNode.RbParent.RbLeft;
                    if (workingNode.Color == TreeNodeColor.Red)
                    {
                        workingNode.Color = TreeNodeColor.Black;
                        fixupNode.RbParent.Color = TreeNodeColor.Red;
                        RightRotate(fixupNode.RbParent);
                        workingNode = fixupNode.RbParent.RbLeft;
                    }

                    if (workingNode.RbRight.Color == TreeNodeColor.Black &&
                        workingNode.RbLeft.Color == TreeNodeColor.Black)
                    {
                        fixupNode.Color = TreeNodeColor.Red;
                        fixupNode = fixupNode.RbParent;
                    }
                    else
                    {
                        if (workingNode.RbLeft.Color == TreeNodeColor.Black)
                        {
                            workingNode.RbRight.Color = TreeNodeColor.Black;
                            workingNode.Color = TreeNodeColor.Red;
                            LeftRotate(workingNode);
                            workingNode = fixupNode.RbParent.RbLeft;
                        }

                        workingNode.Color = fixupNode.RbParent.Color;
                        fixupNode.RbParent.Color = TreeNodeColor.Black;
                        workingNode.RbLeft.Color = TreeNodeColor.Black;
                        RightRotate(fixupNode.RbParent);
                        fixupNode = _root;
                    }
                }
            }

            fixupNode.Color = TreeNodeColor.Black;
        }
    }
}