#region

using Oz.LeetCode.Trees;

#endregion


//WRONG SOLUTION
namespace Oz.LeetCode.Recursion
{
    public class IsValidBstSolver
    {
        public bool IsValidBst(TreeNode root)
        {
            return IsValidBst(root, int.MinValue, int.MaxValue);
        }

        private static bool IsValidBst(TreeNode root, long min, long max)
        {
            return root == null ||
                   min <= max && root.val >= min && root.val <= max &&
                   IsValidBst(root.left, min, (long) root.val - 1) &&
                   IsValidBst(root.right, (long) root.val + 1, max);
        }
    }
}