using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode.Trees;

public class PopulateNextRightSolution
{
    public Node Connect(Node root)
    {
        foreach (var level in LevelOrder(root))
        {
            for (var i = 0; i < level.Count - 1; i++)
            {
                level[i].next = level[i + 1];
            }
        }

        return root;

        static IEnumerable<IList<Node>> LevelOrder(Node root)
        {
            if (root == null)
            {
                return new List<IList<Node>>();
            }

            var queue = new Queue<Node>();
            queue.Enqueue(root);
            var queueCount = 1;
            var result = new List<IList<Node>>();
            while (queue.Any())
            {
                var newQueueCount = 0;
                var levelList = new List<Node>();
                for (var i = 0; i < queueCount; i++)
                {
                    var element = queue.Dequeue();
                    levelList.Add(element);
                    if (element.left != null)
                    {
                        queue.Enqueue(element.left);
                        newQueueCount++;
                    }

                    if (element.right != null)
                    {
                        queue.Enqueue(element.right);
                        newQueueCount++;
                    }
                }

                queueCount = newQueueCount;
                result.Add(levelList);
            }

            return result;
        }
    }
}