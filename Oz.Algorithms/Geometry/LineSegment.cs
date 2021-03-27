namespace Oz.Algorithms.Geometry
{
    public struct LineSegment
    {
        public LineSegment(Point2d start, Point2d end)
        {
            Start = start;
            End = end;
        }

        public Point2d Start { get; }
        public Point2d End { get; }

        public override string ToString()
        {
            return $"[{Start.ToString()} => {End.ToString()}]";
        }

        public static implicit operator Vector2d(LineSegment segment)
        {
            return new(segment.Start, segment.End);
        }
    }
}