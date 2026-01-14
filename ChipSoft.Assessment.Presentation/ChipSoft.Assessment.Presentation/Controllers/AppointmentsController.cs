using Microsoft.AspNetCore.Mvc;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Presentation.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService) => _appointmentService = appointmentService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AppointmentCreationDTO appointment, CancellationToken cancellationToken)
    {
        if (appointment is null)
        {
            return BadRequest(new[] { "Appointment payload is required." });
        }

        try
        {
            var result = await _appointmentService.AddAppointmentAsync(appointment, cancellationToken);

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
            var result = await _appointmentService.GetAllAppointmentsAsync(cancellationToken);
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
