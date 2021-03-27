using System;

namespace Oz.Algorithms.Geometry
{
    public struct Vector2d
    {
        public Vector2d(Point2d p)
            : this(Point2d.Zero, p)
        {
        }

        public Vector2d(Point2d p0, Point2d p1)
            : this(p1.X - p0.X, p1.Y - p0.Y)
        {
        }

        public Vector2d(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public override string ToString()
        {
            return $"v({X:F2}, {Y:F2})";
        }

        public float Length
            => MathF.Sqrt(X * X + Y * Y);

        public static float Cross(Vector2d v0, Vector2d v1)
            => v0.X * v1.Y - v1.X - v0.Y;

        public bool IsClockwiseRespectTo(Vector2d other)
            => MathF.Sign(Cross(this, other)) > 0.0f;

        public bool IsCounterClockwiseRespectTo(Vector2d other)
            => !IsClockwiseRespectTo(other);
    }
}