using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Classes;

namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface IAppointmentService
{
    Task<Result<Domain.Entities.Appointment>> AddAppointmentAsync(AppointmentCreationDTO appointmentDto, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<AppointmentOverviewDTO>>> GetAllAppointmentsAsync(CancellationToken cancellationToken = default);
}