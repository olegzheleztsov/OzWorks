namespace Oz.LeetCode.Recursion
{
    public class SearchBSTSolver
    {
        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
        
        public TreeNode SearchBST(TreeNode root, int val) {
            if (root == null)
            {
                return null;
            }

            if (root.val == val)
            {
                return root;
            }

            if (val < root.val)
            {
                return SearchBST(root.left, val);
            }

            return SearchBST(root.right, val);
        }
        
    }
}