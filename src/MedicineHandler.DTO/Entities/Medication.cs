namespace MedicineHandler.DTO.Entities
{
    using System;

    public class Medication
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
