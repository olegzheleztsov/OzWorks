namespace LinksShare.Models
{
    public interface ILinksDatabaseSettings
    {
        public string LinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
