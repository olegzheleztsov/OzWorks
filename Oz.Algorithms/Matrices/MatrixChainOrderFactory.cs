using System;

namespace Oz.Algorithms.Matrices
{
    public static class MatrixChainOrderFactory
    {
        public static IMatrixChainOrder Create(DynamicProcedureKind dynamicProcedureKind) =>
            dynamicProcedureKind switch
            {
                DynamicProcedureKind.BottomUp => new BottomUpMatrixChainOrder(),
                DynamicProcedureKind.TopDown => new TopDownMatrixChainOrder(),
                _ => throw new ArgumentException($"Unsupported procedure kind: {dynamicProcedureKind}")
            };
    }
}