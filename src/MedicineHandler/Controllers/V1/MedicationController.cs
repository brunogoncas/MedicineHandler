namespace MedicineHandler.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentValidation;
    using MedicineHandler.Application.Services;
    using MedicineHandler.DTO.Entities;
    using MedicineHandler.DTO.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("medications")]
    [ApiController]
    public sealed class MedicationController : ControllerBase
    {
        private readonly IValidator<Medication> validator;

        private readonly IMedicationService merdicationService;

        public MedicationController(IValidator<Medication> validator, IMedicationService merdicationService)
        {
            this.validator = validator;
            this.merdicationService = merdicationService;
        }

        /// <summary>
        /// Gets all medications.
        /// </summary>
        /// <param name="filter">Query filter.</param>
        /// <returns>The list of medications.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medication>>> GetAllMedicationsAsync(
            [FromQuery] MedicationSearchFilter filter)
        {
            try
            {
                var result = await this.merdicationService.GetAllMedicationsAsync(filter);

                return this.Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets a medication.
        /// </summary>
        /// <param name="id">The medication identifier.</param>
        /// <returns>The medication.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Medication>> GetMedicationByIdAsync(string id)
        {
            try
            {
                var result = await this.merdicationService.GetMedicationByIdAsync(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a medication.
        /// </summary>
        /// <param name="medication">The medication.</param>
        /// <returns>The created medication and it's location.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMedicationAsync([FromBody] Medication medication)
        {
            try
            {
                var validationResult = this.validator.Validate(medication);

                if (!validationResult.IsValid)
                {
                    return this.BadRequest();
                }

                var result = await this.merdicationService.CreateMedicationAsync(medication);

                if (!result)
                {
                    return this.StatusCode(StatusCodes.Status409Conflict);
                }

                return this.CreatedAtAction("GetMedicationById", new { id = medication.Id }, null);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes a medication.
        /// </summary>
        /// <param name="id">The medication identifier.</param>
        /// <returns>Nothing.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicationAsync(string id)
        {
            try
            {
                await this.merdicationService.DeleteMedicationAsync(id);

                return this.NoContent();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
