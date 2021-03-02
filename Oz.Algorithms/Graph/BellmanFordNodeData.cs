using System.Text;

namespace Oz.Algorithms.Graph
{
    public sealed class BellmanFordNodeData<T> : DfsSearchNodeData<T>
    {
        public BellmanFordNodeData(T data) : base(data)
        {
        }

        public GraphVertex<BellmanFordNodeData<T>> Parent
        {
            get => Previous as GraphVertex<BellmanFordNodeData<T>>;
            set => Previous = value;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                $"Parent: {(Parent != null ? Parent.Index + "(" + Parent.Data.Data.ToString() + ")" : "NONE")} Distance: {Distance.GetValueWithInf()}");
            stringBuilder.AppendLine($"Value: ({Data.ToString()})");
            return stringBuilder.ToString();
        }
    }
}