namespace MedicineHandler.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MedicineHandler.DTO.Entities;
    using MedicineHandler.DTO.Filters;

    public interface IMedicationService
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync(MedicationSearchFilter? filter);

        Task<Medication?> GetMedicationByIdAsync(string id);

        Task<bool> CreateMedicationAsync(Medication medication);

        Task DeleteMedicationAsync(string medicationId);
    }
}
