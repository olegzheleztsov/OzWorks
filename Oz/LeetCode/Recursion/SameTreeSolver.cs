using System.Collections.Generic;
using Oz.LeetCode.Trees;

namespace Oz.LeetCode.Recursion
{
    public class SameTreeSolver
    {
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                return true;
            }

            if (!Check(p, q))
            {
                return false;
            }

            Queue<TreeNode> pQueue = new Queue<TreeNode>();
            Queue<TreeNode> qQueue = new Queue<TreeNode>();
            pQueue.Enqueue(p);
            qQueue.Enqueue(q);

            while (pQueue.Count > 0)
            {
                p = pQueue.Dequeue();
                q = qQueue.Dequeue();

                if (!Check(p, q))
                {
                    return false;
                }

                if (p != null)
                {
                    if (!Check(p.left, q.left))
                    {
                        return false;
                    }

                    if (p.left != null)
                    {
                        pQueue.Enqueue(p.left);
                        qQueue.Enqueue(q.left);
                    }

                    if (!Check(p.right, q.right))
                    {
                        return false;
                    }

                    if (p.right != null)
                    {
                        pQueue.Enqueue(p.right);
                        qQueue.Enqueue(q.right);
                    }
                }
            }

            return true;
        }

        private bool Check(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                return true;
            }

            if (p == null || q == null)
            {
                return false;
            }

            if (p.val != q.val)
            {
                return false;
            }

            return true;
        }
    }
}