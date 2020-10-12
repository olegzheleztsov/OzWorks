namespace Oz.Algorithms.Numerics
{
    public class GcdFinder
    {
        private readonly int _firstNumber;
        private readonly int _secondNumber;

        public GcdFinder(int firstNumber, int secondNumber)
        {
            _firstNumber = firstNumber;
            _secondNumber = secondNumber;
        }

        public GcdResult Run()
        {
            int b, bst;
            var ast = b = 1;
            var a = bst = 0;
            var c = _firstNumber;
            var d = _secondNumber;

            var q = c / d;
            var r = c % d;

            while (r != 0)
            {
                c = d;
                d = r;
                var t = ast;
                ast = a;
                a = t - q * a;
                t = bst;
                bst = b;
                b = t - q * b;
                q = c / d;
                r = c % d;
            }

            return new GcdResult(_firstNumber, a, _secondNumber, b, d);
        }
    }
}