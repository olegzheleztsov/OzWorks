using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Oz.Algorithms.Numerics;
using Oz.API.Config;
using Oz.API.Logic.Interfaces;

namespace Oz.API.Logic
{
    public class JugProblemSolver
    {
        private readonly JugConfig _config;
        private readonly int _firstCapacity;
        private readonly IOzLogger<JugProblemSolver> _logger;
        private readonly int _secondCapacity;
        private readonly int _targetFillAmount;

        public JugProblemSolver(JugProblemInputs inputs,
            IOzLogger<JugProblemSolver> logger, JugConfig config)
        {
            _firstCapacity = inputs.FirstJugCapacity;
            _secondCapacity = inputs.SecondJugCapacity;
            _targetFillAmount = inputs.TargetFillAmount;
            _config = config;
            _logger = logger;
            if (!IsPossibleToFill()) throw new ArgumentException(nameof(inputs.TargetFillAmount));

            if (inputs.TargetFillAmount > Math.Max(inputs.FirstJugCapacity, inputs.SecondJugCapacity))
            {
                throw new ArgumentException(nameof(inputs.TargetFillAmount));
            }
        }

        private bool IsPossibleToFill()
        {
            var gcdFinder = new GcdFinder(_firstCapacity, _secondCapacity);
            var result = gcdFinder.Run();
            return _targetFillAmount % result.GreaterCommonDivider == 0;
        }

        private bool IsTargetReached(Jug first, Jug second)
        {
            var smallest = first; //first < second ? first : second;
            var biggest = second;
            return smallest.IsFilledOn(_targetFillAmount) && biggest.IsFilledOn(0);
        }

        public (bool success, IEnumerable<JugStep> steps) RunJugProcess()
        {
            var steps = new List<JugStep>();
            var smallJug = new Jug(Math.Min(_firstCapacity, _secondCapacity));
            var bigJug = new Jug(Math.Max(_firstCapacity, _secondCapacity));
            var success = true;

            while (!IsTargetReached(smallJug, bigJug))
            {
                if (!bigJug.ClearIfFull())
                {
                    if (!smallJug.IsEmpty)
                    {
                        smallJug.SetCurrentAmount(bigJug.FillFrom(smallJug));
                    }
                    else
                    {
                        smallJug.FillWhole();
                    }
                }

                var step = new JugStep(smallJug.CurrentAmount, bigJug.CurrentAmount);
                steps.Add(step);
                _logger.LogInformation(step.ToString());
                if (steps.Count < _config.MaxIterations)
                {
                    continue;
                }

                if (IsTargetReached(smallJug, bigJug))
                {
                    continue;
                }
                success = false;
                break;
            }

            return (success, steps);
        }
    }
}