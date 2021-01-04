using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Matrices
{
    public class BottomUpMatrixChainOrder : IMatrixChainOrder
    {
        public (int[,] costs, int[,] sequence) Find(List<FloatMatrix> matrices)
        {
            var dimensions = matrices.GetMatrixCollectionDimensions();
            
            var countOfMatrices = dimensions.Length - 1;
            var costs = (int[,]) Array.CreateInstance(typeof(int), new[] {countOfMatrices, countOfMatrices}, new[] {1, 1});
            var sequence = (int[,]) Array.CreateInstance(typeof(int), new[] {countOfMatrices - 1, countOfMatrices - 1}, new[] {1, 2});
            for (var i = 1; i <= countOfMatrices; i++)
            {
                costs[i, i] = 0;
            }

            for (var l = 2; l <= countOfMatrices; l++)
            {
                for (var i = 1; i <= countOfMatrices - l + 1; i++)
                {
                    var j = i + l - 1;
                    costs[i, j] = int.MaxValue;
                    for (var k = i; k <= j - 1; k++)
                    {
                        var q = costs[i, k] + costs[k + 1, j] + dimensions[i - 1] * dimensions[k] * dimensions[j];
                        if (q < costs[i, j])
                        {
                            costs[i, j] = q;
                            sequence[i, j] = k;
                        }
                    }
                }
            }

            return (costs, sequence);
        }
    }
}