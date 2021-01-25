namespace Oz.Algorithms.DataStructures
{
    public interface IKeyData<T> : IKey
    {
        public T Data { get; }

        void SetData(T data);
    }
}