using Microsoft.AspNetCore.Mvc;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Presentation.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService) => _doctorService = doctorService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DoctorCreationDTO doctor, CancellationToken cancellationToken)
    {
        if (doctor is null)
        {
            return BadRequest(new[] { "Doctor payload is required." });
        }

        try
        {
            var result = await _doctorService.AddDoctorAsync(doctor, cancellationToken);

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
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _doctorService.GetAllDoctorsAsync(cancellationToken);
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
