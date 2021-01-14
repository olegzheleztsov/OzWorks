namespace SimplePages.Config
{
    public interface IGymSettings
    {
        public string GymDatabase { get; }
        public string TrainingCollectionName { get; }
        public string ConnectionString { get; }
    }
}