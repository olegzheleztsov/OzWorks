namespace Oz.Algorithms.Rod.Trees
{
    public static class BinaryNodeParentedDepthExtensions
    {
        public static BinaryNodeParentedDepth<T> FindLcaParentsAndDepths<T>(BinaryNodeParentedDepth<T> node1,
            BinaryNodeParentedDepth<T> node2)
        {
            while (node1.Depth > node2.Depth)
            {
                node1 = node1.Parent;
            }

            while (node2.Depth > node1.Depth)
            {
                node2 = node2.Parent;
            }

            while (node1 != node2)
            {
                node1 = node1.Parent;
                node2 = node2.Parent;
            }

            return node1;
        }
    }
}