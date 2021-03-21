using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    /// <summary>
    /// Check that value can be prime
    /// </summary>
    public class Pseudoprime
    {
        private readonly BigInteger _value;

        public Pseudoprime(BigInteger value)
        {
            _value = value;
        }

        public bool MayBePrime
        {
            get
            {
                var modularExponentiation = new ModularExponentiation(2, _value - 1, _value);
                var exp = modularExponentiation.Value;
                return exp % _value == 1;
            }
        }
    }
}