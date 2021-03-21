using System;
using System.Collections.Generic;
using System.Numerics;

namespace Oz.Algorithms.Polynom
{
    public class ImplementationTest
    {
        public void Run()
        {
            float[] coefs = {1, 2, 3};
            CoefficientPolynomRepresentation representation
                = new CoefficientPolynomRepresentation(coefs, 3);
            var value = representation.GetValue(1);
            Console.WriteLine(value);
            
        }

        public void RunPoint()
        {
            var p1 = new Polynom(new float[] {1, 2, 3}, 3);
            var p2 = new Polynom(new float[] {3, 4, 5, 4}, 4);
            Console.WriteLine((p1 * p2).ToString());
        }
    }
}