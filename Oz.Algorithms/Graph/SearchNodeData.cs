namespace Oz.Algorithms.Graph
{
    public class SearchNodeData<T>
    {
        public GraphNodeColor Color { get; set; } = GraphNodeColor.White;
        public int Distance { get; set; }
        public IGraphVertex Previous { get; set; }
        
        public T Data { get; }

        public SearchNodeData(T data) => Data = data;
    }
}