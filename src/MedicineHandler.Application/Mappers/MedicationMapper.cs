namespace MedicineHandler.Application.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModels = MedicineHandler.Application.DataModels;
    using Dto = MedicineHandler.DTO.Entities;

    public static class MedicationMapper
    {
        public static Dto.Medication ToDto(
            this DomainModels.Medication medication)
        {
            return new Dto.Medication
            {
                Id = medication.ExternalId,
                Name = medication.Name,
                Quantity = medication.Quantity,
                CreatedAt = medication.CreatedAt,
            };
        }

        public static IEnumerable<Dto.Medication> ToDto(
            this IEnumerable<DomainModels.Medication> entries)
        {
            return entries.Select(e => e.ToDto());
        }

        public static DomainModels.Medication ToDomain(
            this Dto.Medication medication)
        {
            return new DomainModels.Medication
            {
                ExternalId = medication.Id,
                Name = medication.Name,
                Quantity = medication.Quantity,
                CreatedAt = medication.CreatedAt,
            };
        }
    }
}
