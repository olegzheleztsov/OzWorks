namespace Oz.LeetCode.Recursion;

public abstract class SearchBstSolver
{
    public TreeNode SearchBst(TreeNode root, int val)
    {
        if (root == null)
        {
            return null;
        }

        return root.Val == val ? root : SearchBst(val < root.Val ? root.Left : root.Right, val);
    }

    public abstract class TreeNode
    {
        public readonly TreeNode Left;
        public readonly TreeNode Right;
        public readonly int Val;

        protected TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            Val = val;
            Left = left;
            Right = right;
        }
    }
}