namespace Tourism.Repository.Models
{
    public class TourismDBSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string BranchCollectionName { get; set; } = string.Empty;
        public string UserProfileCollectionName { get; set; } = string.Empty;
    }
}
