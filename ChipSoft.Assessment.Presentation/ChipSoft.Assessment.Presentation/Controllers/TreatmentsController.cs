using Microsoft.AspNetCore.Mvc;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Presentation.Controllers;

[ApiController]
[Route("api/treatments")]
public class TreatmentsController : ControllerBase
{
    private readonly ITreatmentService _treatmentService;

    public TreatmentsController(ITreatmentService treatmentService) => _treatmentService = treatmentService;

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var types = _treatmentService.GetAllTreatmentTypes();
            return Ok(types);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
