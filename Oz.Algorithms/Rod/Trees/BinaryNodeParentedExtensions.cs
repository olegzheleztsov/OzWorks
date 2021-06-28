namespace Oz.Algorithms.Rod.Trees
{
    public static class BinaryNodeParentedExtensions
    {
        public static BinaryNodeParented<T> FindLcaParentPointers<T>(BinaryNodeParented<T> node1,
            BinaryNodeParented<T> node2)
        {
            var node = node1;
            while (node != null)
            {
                node.Marked = true;
                node = node.Parent;
            }

            BinaryNodeParented<T> lca = null;
            node = node2;
            while (node != null)
            {
                if (node.Marked)
                {
                    lca = node;
                    break;
                }

                node = node.Parent;
            }

            node = node1;
            while (node != null)
            {
                node.Marked = false;
                node = node.Parent;
            }

            return lca;
        }
    }
}