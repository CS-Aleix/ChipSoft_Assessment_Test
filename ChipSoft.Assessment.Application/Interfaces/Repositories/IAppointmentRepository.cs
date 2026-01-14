using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<Result<List<Appointment>>> GetOtherAppointmentsByDate(Appointment appointment, CancellationToken cancellationToken = default);
}
