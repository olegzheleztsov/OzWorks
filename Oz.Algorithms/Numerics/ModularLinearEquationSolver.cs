using System;
using System.Collections.Generic;
using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    /// <summary>
    /// Solve modular equation a*x mod n == b mod n. Find all x that satisfy to equation
    /// </summary>
    public class ModularLinearEquationSolver
    {
        private readonly BigInteger _leftNumber;
        private readonly BigInteger _rightNumber;
        private readonly BigInteger _modulo;

        public ModularLinearEquationSolver(BigInteger leftNumber, BigInteger rightNumber, BigInteger modulo)
        {
            if (_modulo < 0)
            {
                throw new ArgumentException($"modulo should be greater than 0");
            }
            _leftNumber = leftNumber;
            _rightNumber = rightNumber;
            _modulo = modulo;
        }

        public List<BigInteger> Solution
        {
            get
            {
                var (gcd, firstMult, _) = new ExtendedEuclid(_leftNumber, _modulo).Value;
                if (_rightNumber % gcd != 0)
                {
                    return new List<BigInteger>();
                }

                var result = new List<BigInteger>();
                var x0 = (firstMult * (_rightNumber / gcd)) % _modulo;
                for (var i = 0; i < gcd; i++)
                {
                    var x = (x0 + i * (_modulo / gcd)) % _modulo;
                    if (x < 0)
                    {
                        x += _modulo;
                    }
                    result.Add(x);
                }
                

                return result;
            }
        }
    }
}