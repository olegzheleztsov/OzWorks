#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;

#endregion

namespace Oz.Algorithms.Polynom
{
    internal class PointValueRepresentation : IPolynomRepresentation
    {
        internal PointValueRepresentation(IEnumerable<Point> points, int boundDegree)
        {
            if (boundDegree < 1)
            {
                throw new ArgumentException($"Bound degree should be not less than 1. Current value: {boundDegree}");
            }

            Points = new Point[boundDegree];
            var tempPoints = points.ToArray();
            for (var i = 0; i < boundDegree; i++)
            {
                if (i < tempPoints.Length)
                {
                    Points[i] = tempPoints[i];
                }
                else
                {
                    Points[i] = new Point(0.0f, 0.0f);
                }
            }
        }

        public Point[] Points { get; }

        public int BoundDegree => Points.Length;

        public float GetValue(float x)
        {
            float Diff(int k)
            {
                return Points.Where((_, j) => j != k)
                    .Aggregate(1f, (current, t) => current * (float) (Points[k].X.Real - t.X.Real));
            }

            float DiffValue(float targetX, int k)
            {
                return Points.Where((_, j) => j != k).Aggregate(1f, (current, t) => current * (float) (targetX - t.X.Real));
            }

            return Points.Select((t, k) => (float) t.Y.Real * (DiffValue(x, k) / Diff(k))).Sum();
        }

        public static PointValueRepresentation operator +(PointValueRepresentation repr1,
            PointValueRepresentation repr2)
        {
            PointValueRepresentation minRepr, maxRepr;
            if (repr1.BoundDegree < repr2.BoundDegree)
            {
                minRepr = repr1;
                maxRepr = repr2;
            }
            else
            {
                minRepr = repr2;
                maxRepr = repr1;
            }

            var points = new Point[maxRepr.BoundDegree];
            for (var i = 0; i < points.Length; i++)
            {
                if (i < minRepr.BoundDegree)
                {
                    points[i] = new Point(maxRepr.Points[i].X, maxRepr.Points[i].Y + minRepr.Points[i].Y);
                }
                else
                {
                    points[i] = new Point(maxRepr.Points[i].X, maxRepr.Points[i].Y);
                }
            }

            return new PointValueRepresentation(points, points.Length);
        }

        public static PointValueRepresentation operator -(PointValueRepresentation representation)
        {
            var points = new Point[representation.BoundDegree];
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = new Point(representation.Points[i].X, -representation.Points[i].Y);
            }

            return new PointValueRepresentation(points, points.Length);
        }

        public static PointValueRepresentation operator *(PointValueRepresentation first,
            PointValueRepresentation second)
        {
            var maxDegree = Math.Max(first.BoundDegree, second.BoundDegree);
            PointValueRepresentation maxRepr, minRepr;
            if (first.BoundDegree == maxDegree)
            {
                maxRepr = first;
                minRepr = second;
            }
            else
            {
                maxRepr = second;
                minRepr = first;
            }

            var points = new Point[maxDegree];
            for (var i = 0; i < points.Length; i++)
            {
                if (i < minRepr.BoundDegree)
                {
                    points[i] = new Point(maxRepr.Points[i].X, maxRepr.Points[i].Y * minRepr.Points[i].Y);
                }
                else
                {
                    points[i] = new Point(maxRepr.Points[i].X, new Complex(0, 0));
                }
            }

            return new PointValueRepresentation(points, points.Length);
        }

        public static PointValueRepresentation operator -(PointValueRepresentation repr1,
            PointValueRepresentation repr2)
        {
            var repr2Neg = -repr2;
            return repr1 + repr2Neg;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Points);
        }
    }
}