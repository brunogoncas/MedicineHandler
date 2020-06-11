namespace MedicineHandler.UnitTests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using MedicineHandler.Application.DataModels;
    using MedicineHandler.Application.Repositories;
    using MedicineHandler.Application.Services;
    using MedicineHandler.DTO.Filters;
    using Moq;
    using Xunit;
    using Dto = MedicineHandler.DTO.Entities;

    public sealed class MedicationServiceTests
    {
        private readonly Mock<IMedicationRepository> medicationRepositoryMock = new Mock<IMedicationRepository>();

        private readonly MedicationService medicationService;

        public MedicationServiceTests()
        {
            this.medicationService = new MedicationService(this.medicationRepositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task GetAllMedicationsAsync_Success(
            MedicationSearchFilter filter,
            IEnumerable<Medication> medications)
        {
            // Arrange
            this.medicationRepositoryMock
                .Setup(s => s.GetAllMedicationsAsync(filter.Id, filter.Name))
                .ReturnsAsync(medications);

            // Act
            var result = await this.medicationService.GetAllMedicationsAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(medications.Count(), result.Count());
        }

        [Theory]
        [AutoData]
        public async Task GetMedicationByIdAsync_Success(
            string medicationId,
            Medication medication)
        {
            // Arrange
            medication.ExternalId = medicationId;

            this.medicationRepositoryMock
                .Setup(s => s.GetAllMedicationsAsync(medicationId, null))
                .ReturnsAsync(new[] { medication });

            // Act
            var result = await this.medicationService.GetMedicationByIdAsync(medicationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(medication.ExternalId, result!.Id);
            Assert.Equal(medication.Name, result!.Name);
            Assert.Equal(medication.Quantity, result!.Quantity);
            Assert.Equal(medication.CreatedAt, result!.CreatedAt);
        }

        [Theory]
        [AutoData]
        public async Task GetMedicationByIdAsync_NotFound(
            string medicationId,
            Medication medication)
        {
            // Arrange
            medication.ExternalId = medicationId;

            // Act
            var result = await this.medicationService.GetMedicationByIdAsync(medicationId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoData]
        public async Task CreateMedicationAsync_Success(Dto.Medication medication)
        {
            // Act
            var result = await this.medicationService.CreateMedicationAsync(medication);

            // Assert
            Assert.True(result);

            this.medicationRepositoryMock.Verify(
                s => s.CreateMedicationAsync(
                    It.Is<Medication>(
                        m => m.ExternalId == medication.Id &&
                        m.Name == medication.Name &&
                        m.Quantity == medication.Quantity)),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task CreateMedicationAsync_AlreadyExists(Dto.Medication medication, Medication medicationDomain)
        {
            // Arrange
            this.medicationRepositoryMock
                .Setup(s => s.GetAllMedicationsAsync(medication.Id, null))
                .ReturnsAsync(new[] { medicationDomain });

            // Act
            var result = await this.medicationService.CreateMedicationAsync(medication);

            // Assert
            Assert.False(result);

            this.medicationRepositoryMock.Verify(
                s => s.CreateMedicationAsync(
                    It.IsAny<Medication>()),
                Times.Never);
        }

        [Theory]
        [AutoData]
        public async Task DeleteMedicationAsync_Success(string medicationId)
        {
            // Act
            await this.medicationService.DeleteMedicationAsync(medicationId);

            // Assert
            this.medicationRepositoryMock.Verify(s => s.DeleteMedicationAsync(medicationId), Times.Once);
        }
    }
}
