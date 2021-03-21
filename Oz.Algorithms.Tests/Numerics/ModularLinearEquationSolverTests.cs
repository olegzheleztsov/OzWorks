using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class ModularLinearEquationSolverTests
    {
        [Fact]
        public void Should_Solve_Modular_Equation_Correctly()
        {
            var solver = new ModularLinearEquationSolver(14, 30, 100);
            var result = solver.Solution;
            result.OrderBy(v => v).Should().Equal(new List<BigInteger> {45, 95});
        }
    }
}