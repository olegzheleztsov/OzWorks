namespace Oz.Algorithms.Geometry
{
    public struct Point2d
    {
        public Point2d(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public override string ToString()
        {
            return $"({X:F2}, {Y:F2})";
        }

        public static Point2d Zero
            => new(0.0f, 0.0f);
    }
}