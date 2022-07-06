// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;
using System.Text;

namespace Leetcode.Solutions;

public class _297
{

    public static void Test()
    {
        TreeNode t1 = new TreeNode(1);
        TreeNode t2 = new TreeNode(2);
        TreeNode t3 = new TreeNode(3);
        TreeNode t4 = new TreeNode(4);
        TreeNode t5 = new TreeNode(5);

        t1.left = t2;
        t1.right = t3;
        t3.left = t4;
        t3.right = t5;
        Codec codec = new Codec();
        var str = codec.serialize(t1);
        var tResult = codec.deserialize(str);
        Console.WriteLine(str);
    }
    public class Codec {
        
        // Encodes a tree to a single string.
        public string serialize(TreeNode root) 
        {
            if (root == null)
            {
                return string.Empty;
            }

            StringBuilder res = new StringBuilder();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                res.Append(current == null ? " " : current.val.ToString() + ",");
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);
                }
            }
            res = new StringBuilder(res.ToString().Trim().Replace(" ", "#,"));
        
            if (res[res.Length - 1] == ',')
                res.Remove(res.Length - 1, 1);
        
            return res.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (data == null || data == string.Empty)
            {
                return null;
            }
            
            string[] vals = data.Split(',');
            TreeNode res = new TreeNode(Convert.ToInt32(vals[0]));
            Queue<TreeNode> q = new Queue<TreeNode>();
            int i = 1;
        
            q.Enqueue(res);
            while (i < vals.Length)
            {
                TreeNode cur = q.Dequeue();
                TreeNode l = null;
                TreeNode r = null;

                l = vals[i] == "#" ? null : new TreeNode(int.Parse(vals[i]));
                if (l != null)
                {
                    cur.left = l;
                    q.Enqueue(l);
                }

                if (++i == vals.Length)
                {
                    break;
                }
                
                r = vals[i] == "#" ? null : new TreeNode(Convert.ToInt32(vals[i]));
                if (r != null)
                {
                    cur.right = r;
                    q.Enqueue(r);
                }
            
                i++;
            }

            return res;
        }
    }
}