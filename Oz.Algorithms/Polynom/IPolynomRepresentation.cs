namespace Oz.Algorithms.Polynom
{
    internal interface IPolynomRepresentation
    {
        int BoundDegree { get; }
        float GetValue(float x);
    }
}