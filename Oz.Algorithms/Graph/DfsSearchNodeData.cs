namespace Oz.Algorithms.Graph
{
    public class DfsSearchNodeData<T> : BfsSearchNodeData<T>
    {
        public DfsSearchNodeData(T data) : base(data)
        {
        }
        

        public int FinalDistance { get; set; }
    }
}