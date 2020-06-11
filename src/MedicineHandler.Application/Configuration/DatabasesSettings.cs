namespace MedicineHandler.Application.Configuration
{
    public sealed class DatabasesSettings
    {
        public string MongoDBConnectionString { get; set; } = null!;

        public string MongoDBName { get; set; } = null!;
    }
}
