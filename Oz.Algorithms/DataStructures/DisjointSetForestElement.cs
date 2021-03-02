namespace Oz.Algorithms.DataStructures
{
    public class DisjointSetForestElement<T>
    {
        public DisjointSetForestElement<T> Parent { get; private set; }
        public int Rank { get; private set; }
        public T Data { get; }


        public void SetParent(DisjointSetForestElement<T> parent) => Parent = parent;

        public void SetRank(int rank) => Rank = rank;

        private DisjointSetForestElement(T data)
            => Data = data;

        public static DisjointSetForestElement<T> MakeSet(T data)
        {
            var root = new DisjointSetForestElement<T>(data);
            root.SetParent(root);
            root.SetRank(0);
            return root;
        }

        public static DisjointSetForestElement<T> Union(DisjointSetForestElement<T> first,
            DisjointSetForestElement<T> second)
        {
            return Link(FindSet(first), FindSet(second));
        }

        private static DisjointSetForestElement<T> FindSet(DisjointSetForestElement<T> element)
        {
            while (element != element.Parent)
            {
                element.SetParent(FindSet(element.Parent));
            }

            return element.Parent;
        }

        private static DisjointSetForestElement<T> Link(DisjointSetForestElement<T> first, DisjointSetForestElement<T> second)
        {
            if (first.Rank > second.Rank)
            {
                second.SetParent(first);
                return first;
            }
            else
            {
                first.SetParent(second);
                if (first.Rank == second.Rank)
                {
                    second.SetRank(second.Rank + 1);
                }

                return second;
            }
        }
    }
}