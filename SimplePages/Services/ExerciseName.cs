namespace SimplePages.Services
{
    public class ExerciseName
    {
        public int Id { get; }
        public string Name { get; }

        public ExerciseName(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}