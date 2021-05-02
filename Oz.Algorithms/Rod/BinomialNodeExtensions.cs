using System;

namespace Oz.Algorithms.Rod
{
    public static class BinomialNodeExtensions
    {
        public static BinomialNode<T> MergeTrees<T>(BinomialNode<T> root1, BinomialNode<T> root2, Comparison<T> comparison)
        {
            if (comparison(root1.Data, root2.Data) < 0)
            {
                root2.NextSibling = root1.FirstChild;
                root1.FirstChild = root2;
                root1.Order++;
                return root1;
            }

            root1.NextSibling = root2.FirstChild;
            root2.FirstChild = root1;
            root2.Order++;
            return root2;
        }

        public static void Visit<T>(this BinomialNode<T> root, Action<T, int> visitor, int level = 0)
        {
            var pointer = root;
            while (pointer != null)
            {
                visitor.Invoke(pointer.Data, level);
                pointer.FirstChild?.Visit(visitor, level + 1);
                pointer = pointer.NextSibling;
            }
        }
        
        public static void Visit<T>(this BinomialNode<T> root, Action<BinomialNode<T>, int> visitor, int level = 0)
        {
            var pointer = root;
            while (pointer != null)
            {
                visitor.Invoke(pointer, level);
                pointer.FirstChild?.Visit(visitor, level + 1);
                pointer = pointer.NextSibling;
            }
        }
        
    }
}