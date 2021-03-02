namespace Oz.Algorithms.DataStructures
{
    public class DisjointSetElement<T>
    {
        public DisjointSetElement(T data, DisjointSet<T> setRef)
        {
            Data = data;
            OwnerSet = setRef;
        }
        public T Data { get;  }
        public DisjointSet<T> OwnerSet { get;  set; }

        public DisjointSetElement<T> Next { get; set; }

    }
}