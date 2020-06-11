namespace MedicineHandler.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MedicineHandler.Application.Mappers;
    using MedicineHandler.Application.Repositories;
    using MedicineHandler.DTO.Filters;
    using Dto = MedicineHandler.DTO.Entities;

    public sealed class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository medicationRepository;

        public MedicationService(IMedicationRepository medicationRepository)
        {
            this.medicationRepository = medicationRepository;
        }

        public async Task<IEnumerable<Dto.Medication>> GetAllMedicationsAsync(MedicationSearchFilter? filter)
        {
            var result = await this.medicationRepository.GetAllMedicationsAsync(filter?.Id, filter?.Name);

            return result.ToDto();
        }

        public async Task<Dto.Medication?> GetMedicationByIdAsync(string id)
        {
            var result = await this.medicationRepository.GetAllMedicationsAsync(medicationId: id);

            return result.Any() ? result.First().ToDto() : null;
        }

        public async Task<bool> CreateMedicationAsync(Dto.Medication medication)
        {
            var result = await this.medicationRepository.GetAllMedicationsAsync(medicationId: medication.Id);

            if (result.Any())
            {
                return false;
            }

            var medicationDomain = medication.ToDomain();
            medicationDomain.Id = Guid.NewGuid();

            await this.medicationRepository.CreateMedicationAsync(medicationDomain);

            return true;
        }

        public async Task DeleteMedicationAsync(string medicationId)
        {
            await this.medicationRepository.DeleteMedicationAsync(medicationId);
        }
    }
}
