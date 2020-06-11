namespace MedicineHandler.Application.Configuration
{
    public sealed class Settings : ISettings
    {
        public DatabasesSettings Databases { get; set; } = null!;
    }
}
