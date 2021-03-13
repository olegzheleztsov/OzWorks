namespace Oz.Algorithms.Matrices
{
    public record LinearSystemResult(
            FloatMatrix Upper, 
            FloatMatrix Lower, 
            IntegerMatrix Permutation, 
            float[] Solution);
}