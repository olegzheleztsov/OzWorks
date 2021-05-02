namespace ToolIDE
{
    public class Book {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int YearPublished { get; set; }

        public override string ToString()
        {
            return $"{Name} by {Author}\nPublished {YearPublished}";
        }
    }
}