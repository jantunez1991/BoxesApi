using BoxesApi.Models;
using BoxesApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BoxesApi.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;
        private readonly IValidator<Lead> _validator;
        private static readonly List<Lead> _leads = new();

        public LeadsController(IWorkshopService workshopService, IValidator<Lead> validator)
        {
            _workshopService = workshopService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] Lead lead)
        {
            var validationResult = await _validator.ValidateAsync(lead);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var isValidPlace = await _workshopService.IsValidPlaceAsync(lead.PlaceId);
            if (!isValidPlace)
                return BadRequest("El place_id no es válido.");

            _leads.Add(lead); // mock en memoria
            return Created("", lead);
        }
    }
}
