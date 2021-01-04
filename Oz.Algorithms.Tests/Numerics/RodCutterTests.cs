using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class RodCutterTests
    {
        [Theory]
        [InlineData(0, 0.0)]
        [InlineData(1, 1.0)]
        [InlineData(2, 5.0)]
        [InlineData(3, 8.0)]
        [InlineData(4, 10.0)]
        [InlineData(5, 13.0)]
        [InlineData(6, 17.0)]
        [InlineData(7, 18.0)]
        [InlineData(8, 22.0)]
        [InlineData(9, 25.0)]
        [InlineData(10, 30.0)]
        public void Should_Correctly_Cut_Rod(int rodLength, double expectedOptimalPrice)
        {
            double[] prices = {1, 5, 8, 9, 10, 17, 17, 20, 24, 30};
            var rodCutter = new RodCutter(prices);
            Assert.Equal(expectedOptimalPrice, rodCutter.MemoizedCurRod(rodLength));
        }

        [Theory]
        [InlineData(0, 0.0)]
        [InlineData(1, 1.0)]
        [InlineData(2, 5.0)]
        [InlineData(3, 8.0)]
        [InlineData(4, 10.0)]
        [InlineData(5, 13.0)]
        [InlineData(6, 17.0)]
        [InlineData(7, 18.0)]
        [InlineData(8, 22.0)]
        [InlineData(9, 25.0)]
        [InlineData(10, 30.0)]
        public void Should_Correctly_Cur_With_Bottom_Up_Cut(int rodLength, double expectedOptimalPrice)
        {
            double[] prices = {1, 5, 8, 9, 10, 17, 17, 20, 24, 30};
            var rodCutter = new RodCutter(prices);
            Assert.Equal(expectedOptimalPrice, rodCutter.BottomUpCutRod(rodLength));
        }
    }
}