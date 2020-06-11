namespace MedicineHandler.Application.Validation
{
    using FluentValidation;
    using MedicineHandler.DTO.Entities;

    public sealed class MedicationValidator : AbstractValidator<Medication>
    {
        public MedicationValidator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            this.RuleFor(m => m.Id).NotEmpty();

            this.RuleFor(m => m.Name).NotEmpty();

            this.RuleFor(m => m.Quantity).GreaterThan(0);
        }
    }
}
