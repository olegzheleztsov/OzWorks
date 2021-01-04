using System.Collections.Generic;

namespace Oz.Algorithms.Matrices
{
    public interface IMatrixChainOrder
    {
        (int[,] costs, int[,] sequence) Find(List<FloatMatrix> matrices);
    }
}