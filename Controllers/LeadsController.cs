using BoxesApi.Models;
using BoxesApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BoxesApi.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadsController(
    IWorkshopService workshopService,
    IValidator<Lead> validator,
    ILogger<LeadsController> logger)
    : ControllerBase
{
    private static readonly List<Lead> _leads = new();

    [HttpPost]
    public async Task<IActionResult> CreateLead([FromBody] Lead lead)
    {
        logger.LogInformation("Nuevo lead recibido para place_id {PlaceId} - {ServiceType}", lead.PlaceId, lead.ServiceType);

        var validationResult = await validator.ValidateAsync(lead);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Errores de validación: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var isValidPlace = await workshopService.IsValidPlaceAsync(lead.PlaceId);
        if (!isValidPlace)
        {
            logger.LogWarning("place_id inválido: {PlaceId}", lead.PlaceId);
            return BadRequest("El place_id no es válido.");
        }

        _leads.Add(lead);
        logger.LogInformation("Lead registrado exitosamente. Total acumulado: {Count}", _leads.Count);
        return Created("", lead);
    }
}