namespace Oz.Algorithms.Matrices
{
    /// <summary>
    ///     Represent one of the computation methods in dynamic programming
    /// </summary>
    public enum DynamicProcedureKind
    {
        /// <summary>
        ///     It's usually recursive top down procedure with memoizing
        /// </summary>
        TopDown,

        /// <summary>
        ///     It's bottom up procedure that first computes base cases and after computes other cases using base cases
        /// </summary>
        BottomUp
    }
}