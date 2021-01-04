using System.Collections.Generic;
using Oz.Algorithms.Matrices;
using Xunit;

namespace Oz.Algorithms.Tests.Matrices
{
    public class MatrixChainOrderFactoryTests
    {
        [Fact]
        public void Both_Procedure_Kinds_Should_Lead_To_The_Same_Result()
        {
            var matrices = new List<FloatMatrix>()
            {
                new FloatMatrix(30, 35),
                new FloatMatrix(35, 15),
                new FloatMatrix(15, 5),
                new FloatMatrix(5, 10),
                new FloatMatrix(10, 20),
                new FloatMatrix(20, 25)
            };

            var chainOrder1 = MatrixChainOrderFactory.Create(DynamicProcedureKind.BottomUp);
            var (cost1, sequence1) = chainOrder1.Find(matrices);

            var chainOrder2 = MatrixChainOrderFactory.Create(DynamicProcedureKind.TopDown);
            var (cost2, sequence2) = chainOrder2.Find(matrices);
            
            Assert.Equal(cost1.AsEnumerable(), cost2.AsEnumerable());
            Assert.Equal(sequence1.AsEnumerable(), sequence2.AsEnumerable());
        }
    }
}