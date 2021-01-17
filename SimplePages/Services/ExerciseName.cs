using SimplePages.Models.GymStats;

namespace SimplePages.Services
{
    public class ExerciseName
    {
        public ExerciseName(int id, string name, BodyPart bodyPart)
        {
            Id = id;
            Name = name;
            BodyPart = bodyPart;
        }

        public int Id { get; }
        public string Name { get; }

        public BodyPart BodyPart { get; }
    }
}