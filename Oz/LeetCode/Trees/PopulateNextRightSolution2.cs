namespace Oz.LeetCode.Trees;

public class PopulateNextRightSolution2
{
    public Node Connect(Node root)
    {
        if (root == null)
        {
            return null;
        }

        if (root.left != null)
        {
            if (root.right != null)
            {
                root.left.next = root.right;
            }
            else
            {
                var pt = root.next;
                while (pt != null)
                {
                    if (pt.left != null)
                    {
                        root.left.next = pt.left;
                        break;
                    }

                    if (pt.right != null)
                    {
                        root.left.next = pt.right;
                        break;
                    }

                    pt = pt.next;
                }
            }
        }

        if (root.right != null)
        {
            var pt = root.next;
            while (pt != null)
            {
                if (pt.left != null)
                {
                    root.right.next = pt.left;
                    break;
                }

                if (pt.right != null)
                {
                    root.right.next = pt.right;
                    break;
                }

                pt = pt.next;
            }
        }

        if (root.left != null)
        {
            Connect(root.left);
        }

        if (root.right != null)
        {
            Connect(root.right);
        }

        return root;
    }
}