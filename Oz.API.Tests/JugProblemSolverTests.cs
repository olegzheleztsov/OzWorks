using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Oz.API.Config;
using Oz.API.Logic;
using Oz.API.Logic.Interfaces;
using Xunit;

namespace Oz.API.Tests
{
    public class JugProblemSolverTests
    {
        [Fact]
        public void Should_Find_Jug_Problem_Solution_Correctly()
        {
            var inputs = new JugProblemInputs(21, 26, 3);
            var config = new JugConfig {MaxIterations = 100};
            var moqLoggerFactory = new Mock<ILoggerFactory>();
            var moqLogger = new Mock<IOzLogger<JugProblemSolver>>();
            moqLogger.Setup(logger => logger.LogInformation(It.IsAny<string>())).Callback(() => { });

            var solver = new JugProblemSolver(inputs, moqLogger.Object, config);
            var (result, steps) = solver.RunJugProcess();
            Assert.True(result);
            Assert.True(steps.Any());
            Assert.True(steps.Last().SecondAmount == 0);
        }

        [Fact]
        public void Should_Throw_Exception_When_Jugs_Cannot_Be_Completed()
        {
            var inputs = new JugProblemInputs(20, 30, 15);
            var config = new JugConfig {MaxIterations = 100};
            var moqLoggerFactory = new Mock<ILoggerFactory>();
            var moqLogger = new Mock<IOzLogger<JugProblemSolver>>();
            moqLogger.Setup(logger => logger.LogInformation(It.IsAny<string>())).Callback(() => { });
            Assert.Throws<ArgumentException>(() =>
            {
                var solver = new JugProblemSolver(inputs, moqLogger.Object, config);
            });
        }

        [Fact]
        public void Should_Succeed_When_Equal_Capacity_And_Target_Is_Capacity()
        {
            var (inputs, config, logger) = ConfigureInputs(20, 20, 20);
            var solver = new JugProblemSolver(inputs, logger, config);
            var (success, steps) = solver.RunJugProcess();
            Assert.True(success);
            Assert.True(steps.Any());
            Assert.True(steps.Last().SecondAmount == 0);
            Assert.Equal(20, steps.Last().FirstAmount);
        }

        [Fact]
        public void Should_Fail_When_Equal_Capacity_But_Target_Is_Not_Gcd()
        {
            var (inputs, config, logger) = ConfigureInputs(20, 20, 15);

            Assert.Throws<ArgumentException>(() =>
            {
                var solver = new JugProblemSolver(inputs, logger, config);
            });
        }

        private (JugProblemInputs, JugConfig, IOzLogger<JugProblemSolver>) ConfigureInputs(int firstCapacity,
            int secondCapacity, int targetAmount)
        {
            var inputs = new JugProblemInputs(firstCapacity, secondCapacity, targetAmount);
            var config = new JugConfig {MaxIterations = 100};
            var moqLoggerFactory = new Mock<ILoggerFactory>();
            var moqLogger = new Mock<IOzLogger<JugProblemSolver>>();
            moqLogger.Setup(logger => logger.LogInformation(It.IsAny<string>())).Callback(() => { });
            return (inputs, config, moqLogger.Object);
        }
    }
}