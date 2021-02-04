using System.Collections.Generic;

namespace Oz.Algorithms.Graph
{
    public class ConnectedComponentsData<T>
    {
        private readonly List<List<GraphVertex<DfsSearchNodeData<T>>>> _connectedComponents =
            new List<List<GraphVertex<DfsSearchNodeData<T>>>>();

        public List<List<GraphVertex<DfsSearchNodeData<T>>>> ConnectedComponents => _connectedComponents;

        private List<GraphVertex<DfsSearchNodeData<T>>> _aggregateComponent =
            new List<GraphVertex<DfsSearchNodeData<T>>>();

        public void StartNewComponent()
        {
            if (_aggregateComponent.Count > 0)
            {
                _connectedComponents.Add(_aggregateComponent);
            }

            _aggregateComponent = new List<GraphVertex<DfsSearchNodeData<T>>>();
        }

        public void Aggregate(GraphVertex<DfsSearchNodeData<T>> vertex)
        {
            _aggregateComponent.Add(vertex);
        }
    }
}