using Microsoft.AspNetCore.Mvc;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Presentation.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService) => _patientService = patientService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PatientCreationDTO patient, CancellationToken cancellationToken)
    {
        if (patient is null)
        {
            return BadRequest(new[] { "Patient payload is required." });
        }

        try
        {
            var result = await _patientService.AddPatientAsync(patient, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Created(string.Empty, result.Data);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return Problem("Request cancelled.", statusCode: 499);
        }
        catch (Exception ex)
        {
            // In a real app inject ILogger and log the exception.
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _patientService.GetAllPatientsAsync(cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return Problem("Request cancelled.", statusCode: 499);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}