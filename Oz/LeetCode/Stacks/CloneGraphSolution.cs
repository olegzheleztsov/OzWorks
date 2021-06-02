#region

using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Stacks
{
    public class CloneGraphSolution
    {
        public Node CloneGraph(Node node)
        {
            if (node == null)
            {
                return null;
            }

            var newRoot = new Node(node.val);

            if (node.neighbors.Count == 0)
            {
                return newRoot;
            }

            HashSet<Node> cloned = new HashSet<Node>() {node};
            Node[] newNodeArr = new Node[101];
            newNodeArr[newRoot.val] = newRoot;
            CloneGraphImplementation(node, newRoot, cloned, newNodeArr);

            foreach (var n in cloned)
            {
                foreach (var ch in n.neighbors)
                {
                    newNodeArr[n.val].neighbors.Add(newNodeArr[ch.val]);
                }
            }
            
            return newRoot;
        }

        private void CloneGraphImplementation(Node current, Node currentCopy, HashSet<Node> cloned, Node[] newNodeArr)
        {
            foreach (var neighbor in current.neighbors)
            {
                if (!cloned.Contains(neighbor))
                {
                    cloned.Add(neighbor);
                    var newCloneNode = new Node(neighbor.val);
                    newNodeArr[newCloneNode.val] = newCloneNode;
                    CloneGraphImplementation(neighbor, newCloneNode, cloned, newNodeArr);
                }
            }
        }

        public class Node
        {
            public IList<Node> neighbors;
            public int val;

            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }
    }
}