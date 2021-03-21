using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    /// <summary>
    /// Find a^b mod n
    /// </summary>
    public class ModularExponentiation
    {
        private readonly BigInteger _number;
        private readonly BigInteger _degree;
        private readonly BigInteger _modulo;

        public ModularExponentiation(BigInteger number, BigInteger degree, BigInteger modulo)
        {
            _number = number;
            _degree = degree;
            _modulo = modulo;
        }

        public BigInteger Value
        {
            get
            {
                BigInteger c = 0;
                BigInteger d = 1;
                var binaryRepresentation = _degree.ToBinaryString().TrimStart('0');

                foreach (var symbol in binaryRepresentation)
                {
                    c = 2 * c;
                    d = (d * d) % _modulo;
                    if (symbol == '1')
                    {
                        c++;
                        d = (d * _number) % _modulo;
                    }
                }

                return d;
            }
        }
    }
}