using System.Text;

namespace Oz.Algorithms.Rod
{
    /// <summary>
    /// Node of BinomialHeal
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinomialNode<T>
    {
        public BinomialNode(T data, BinomialNode<T> nextSibling = default, BinomialNode<T> firstChild = default)
        {
            Data = data;
            NextSibling = nextSibling;
            FirstChild = firstChild;
        }

        public BinomialNode<T> NextSibling { get; set; }
        public BinomialNode<T> FirstChild { get; set; }
        public T Data { get; }
        
        public int Order { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Data);
            
            if (FirstChild != null)
            {
                stringBuilder.Append($" :C| {FirstChild.Data}");
            }
            
            if (NextSibling != null)
            {
                stringBuilder.Append($" :N-> {NextSibling.Data}");
            }



            return stringBuilder.ToString();
        }
    }
}