namespace SimplePages.Config
{
    public class GymSettings : IGymSettings
    {
        public string GymDatabase { get; set; }
        public string TrainingCollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}