namespace MedicineHandler.Application.DataModels
{
    using System;

    public sealed class Medication
    {
        public Guid Id { get; set; }

        public string ExternalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
