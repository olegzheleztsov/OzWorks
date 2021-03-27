using System;

namespace Oz.Algorithms.Geometry
{
    public static class GeometryUtils
    {
        public static bool IsLineSegmentsIntersect(LineSegment first, LineSegment second)
        {
            var d1 = Direction(second.Start, second.End, first.Start);
            var d2 = Direction(second.Start, second.End, first.End);
            var d3 = Direction(first.Start, first.End, second.Start);
            var d4 = Direction(first.Start, first.End, second.End);
            if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
                ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
            {
                return true;
            }

            if (Util.Approximately(d1, 0.0f) && OnSegment(second, first.Start))
            {
                return true;
            }

            if (Util.Approximately(d2, 0.0f) && OnSegment(second, first.End))
            {
                return true;
            }

            if (Util.Approximately(d3, 0.0f) && OnSegment(first, second.Start))
            {
                return true;
            }

            if (Util.Approximately(d4, 0.0f) && OnSegment(first, second.End))
            {
                return true;
            }

            return false;
        }

        private static float Direction(Point2d first, Point2d second, Point2d third)
        {
            var v0 = new Vector2d(first, third);
            var v1 = new Vector2d(first, second);
            return Vector2d.Cross(v0, v1);
        }

        private static bool OnSegment(LineSegment segment, Point2d point2d)
        {
            return MathF.Min(segment.Start.X, segment.End.X) <= point2d.X &&
                   point2d.X <= MathF.Max(segment.Start.X, segment.End.X) &&
                   MathF.Min(segment.Start.Y, segment.End.Y) <= point2d.Y &&
                   point2d.Y <= MathF.Max(segment.Start.Y, segment.End.Y);
        }
    }
}