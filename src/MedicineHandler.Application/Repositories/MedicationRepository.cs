namespace MedicineHandler.Application.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MedicineHandler.Application.DataModels;
    using MongoDB.Driver;

    public sealed class MedicationRepository : IMedicationRepository
    {
        public const string CollectionName = "medications";

        private readonly IMongoCollection<Medication> collection;

        public MedicationRepository(IMongoDatabase database)
        {
            this.collection = database.GetCollection<Medication>(CollectionName);
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync(
            string? medicationId,
            string? name = null)
        {
            var builder = Builders<Medication>.Filter;
            FilterDefinition<Medication> filterDefinition = Builders<Medication>.Filter.Empty;

            if (medicationId != null)
            {
                filterDefinition &= builder.Eq(b => b.ExternalId, medicationId);
            }

            if (name != null)
            {
                filterDefinition &= builder.Eq(b => b.Name, name);
            }

            return await this.collection
                .FindSync(filterDefinition)
                .ToListAsync();
        }

        public async Task CreateMedicationAsync(Medication medication)
        {
            await this.collection.InsertOneAsync(medication);
        }

        public async Task DeleteMedicationAsync(string medicationId)
        {
            var builder = Builders<Medication>.Filter;
            var filterDefinition = builder.Eq(b => b.ExternalId, medicationId);

            await this.collection.DeleteManyAsync(filterDefinition);
        }
    }
}
