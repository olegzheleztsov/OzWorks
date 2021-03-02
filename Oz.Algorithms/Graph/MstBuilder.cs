using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Graph
{
    public class MstBuilder<T>
    {
        private readonly IGraph<T> _graph;

        private readonly List<int> _mstVertices = new();
        private readonly List<IEdge<T>> _mstEdges = new();
        private readonly int[] _graphVertices;

        public MstBuilder(IGraph<T> graph)
        {
            _graph = graph;
            _graphVertices = _graph.Vertices.Select(v => v.Index).Distinct().ToArray();
        }

        public (List<int>, List<IEdge<T>>) Build()
        {
            _mstVertices.Clear();
            _mstEdges.Clear();
            while (!IsEdgesFormMst())
            {
                var safeEdge = FindSafeEdge();
                if (safeEdge == null)
                {
                    break;
                }
                _mstEdges.Add(safeEdge);
                AddMstVertex(safeEdge.FromIndex);
                AddMstVertex(safeEdge.ToIndex);
            }

            return (_mstVertices.OrderBy(v => v).ToList(), _mstEdges);
        }

        private bool IsEdgesFormMst()
        {
            var lst = _mstEdges.SelectMany(e => new[] {e.FromIndex, e.ToIndex}).Distinct().ToList();
            return lst.Count == _graphVertices.Length;
        }

        private bool IsMstContainsEdge(IEdge<T> edge)
        {
            foreach (var mstEdge in _mstEdges)
            {
                if (edge.FromIndex == mstEdge.FromIndex && edge.ToIndex == mstEdge.ToIndex)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsVertexInMst(int vertexIndex)
        {
            return _mstVertices.Contains(vertexIndex);
        }

        private bool IsCutEdge(IEdge<T> edge)
        {
            if (_mstVertices.Count == 0)
            {
                return true;
            }
            
            return (IsVertexInMst(edge.FromIndex) && !IsVertexInMst(edge.ToIndex))
                   || (IsVertexInMst(edge.ToIndex) && !IsVertexInMst(edge.FromIndex));
        }

        private IEdge<T> FindSafeEdge()
        {
            var cutEdges = _graph
                .Edges
                .Where(wEdge => !IsMstContainsEdge(wEdge) && IsCutEdge(wEdge))
                .ToList();
            return cutEdges.Count > 0 
                ? cutEdges
                    .OrderBy(cEdge => _graph.WeightFunc(cEdge.FromVertex, cEdge.ToVertex))
                    .First() 
                : null;
        }

        private void AddMstVertex(int index)
        {
            if (!_mstVertices.Contains(index))
            {
                _mstVertices.Add(index);
            }
        }
    }
}