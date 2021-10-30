namespace MP.DocumentDB.Models
{
    public class CollectionConfiguration<T> where T : class
    {
        public string? CollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}