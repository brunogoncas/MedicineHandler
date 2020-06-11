namespace MedicineHandler.Application.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MedicineHandler.Application.DataModels;

    public interface IMedicationRepository
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync(
            string? medicationId,
            string? name = null);

        Task CreateMedicationAsync(Medication medication);

        Task DeleteMedicationAsync(string medicationId);
    }
}
