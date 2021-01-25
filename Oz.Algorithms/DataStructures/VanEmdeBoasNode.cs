using System;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    public class VanEmdeBoasNode
    {
        public VanEmdeBoasNode(int size)
        {
            Size = size;
            switch (size)
            {
                case > 2:
                {
                    var upperSubSize = VanEmdeBoasUtils.UpperSquareRoot(size);
                    var lowerSubSize = VanEmdeBoasUtils.LowerSquareRoot(size);
                    Summary = new VanEmdeBoasNode(upperSubSize);
                    Cluster = new VanEmdeBoasNode[upperSubSize];
                    for (var i = 0; i < Cluster.Length; i++)
                    {
                        Cluster[i] = new VanEmdeBoasNode(lowerSubSize);
                    }

                    break;
                }
                case 2:
                    Summary = null;
                    Cluster = null;
                    break;
                default:
                    throw new ArgumentException("Size can't be less than 2");
            }
        }

        public int Size { get; }
        public int? Minimum { get; set; }
        public int? Maximum { get; set; }
        public VanEmdeBoasNode Summary { get; }
        public VanEmdeBoasNode[] Cluster { get; }

        public int High(int key)
        {
            return VanEmdeBoasUtils.High(key, Size);
        }

        public int Low(int key)
        {
            return VanEmdeBoasUtils.Low(key, Size);
        }

        public int Index(int x, int y)
        {
            return VanEmdeBoasUtils.Index(x, y, Size);
        }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            ConstructString(0, stringBuilder, this);
            return stringBuilder.ToString();
        }

        private static void ConstructString(int level, StringBuilder stringBuilder, VanEmdeBoasNode node)
        {
            const int levelIndent = 4;
            stringBuilder.AppendLine($"{new string(' ', level)}Size: {node.Size}, Minimum: {node.Minimum}, Maximum: {node.Maximum}");
            if (node.Size > 2)
            {
                stringBuilder.AppendLine($"{new string(' ', level)}SUMMARY");
                ConstructString(level + levelIndent, stringBuilder, node.Summary);
                stringBuilder.AppendLine($"{new string(' ', level)}Cluster:");
                foreach (var cl in node.Cluster)
                {
                    ConstructString(level + levelIndent, stringBuilder, cl);
                }
            }
            else
            {
                stringBuilder.AppendLine($"{new string(' ', level)}LEAF");
            }
        }
    }
}