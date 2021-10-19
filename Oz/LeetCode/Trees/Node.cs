namespace Oz.LeetCode.Trees;

public class Node
{
    public Node left;
    public Node next;
    public Node right;
    public int val;

    public Node()
    {
    }

    public Node(int _val) =>
        val = _val;

    public Node(int _val, Node _left, Node _right, Node _next)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}