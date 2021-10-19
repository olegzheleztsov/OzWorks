#region

using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Stacks;

public class CloneGraphSolution
{
    public Node CloneGraph(Node node)
    {
        if (node == null)
        {
            return null;
        }

        var newRoot = new Node(node.Value);

        if (node.NeighborValues.Count == 0)
        {
            return newRoot;
        }

        var cloned = new HashSet<Node> {node};
        var newNodeArr = new Node[101];
        newNodeArr[newRoot.Value] = newRoot;
        CloneGraphImplementation(node, cloned, newNodeArr);

        foreach (var n in cloned)
        {
            foreach (var ch in n.NeighborValues)
            {
                newNodeArr[n.Value].NeighborValues.Add(newNodeArr[ch.Value]);
            }
        }

        return newRoot;
    }

    private void CloneGraphImplementation(Node current, HashSet<Node> cloned, Node[] newNodeArr)
    {
        foreach (var neighbor in current.NeighborValues)
        {
            if (cloned.Contains(neighbor))
            {
                continue;
            }

            cloned.Add(neighbor);
            var newCloneNode = new Node(neighbor.Value);
            newNodeArr[newCloneNode.Value] = newCloneNode;
            CloneGraphImplementation(neighbor, cloned, newNodeArr);
        }
    }

    public class Node
    {
        public readonly IList<Node> NeighborValues;
        public readonly int Value;

        public Node()
        {
            Value = 0;
            NeighborValues = new List<Node>();
        }

        public Node(int value)
        {
            Value = value;
            NeighborValues = new List<Node>();
        }

        public Node(int value, IList<Node> neighborValues)
        {
            Value = value;
            NeighborValues = neighborValues;
        }
    }
}