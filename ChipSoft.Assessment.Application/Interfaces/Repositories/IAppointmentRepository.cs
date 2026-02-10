using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task<Result<Appointment>> AddAsync(Appointment appointment, CancellationToken cancellationToken);
    Task<Result<List<Appointment>>> GetAllAsync(CancellationToken cancellationToken);
}
