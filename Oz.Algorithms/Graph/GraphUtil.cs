using System;

namespace Oz.Algorithms.Graph
{
    public class GraphUtil
    {
        public static int AddGraphWeights(int first, int second)
        {
            if (!Util.IsInfinity(first) && !Util.IsInfinity(second))
            {
                return first + second;
            }
            
            if (Util.IsInfinity(first) && !Util.IsInfinity(second))
            {
                return first;
            }

            if (!Util.IsInfinity(first) && Util.IsInfinity(second))
            {
                return second;
            }

            if (Util.IsPositiveInfinity(first) && Util.IsPositiveInfinity(second))
            {
                return Util.IntegerPositiveInfinity;
            }

            if (Util.IsNegativeInfinity(first) && Util.IsNegativeInfinity(second))
            {
                return Util.IntegerNegativeInfinity;
            }

            throw new ArgumentException($"Both values can't be infinity");
        }
    }
}