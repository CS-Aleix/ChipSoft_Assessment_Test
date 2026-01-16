using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IDoctorRepository
{
    Task<Result<Doctor>> AddAsync(Doctor doctor, CancellationToken cancellationToken);
    Task<Result<List<Doctor>>> GetAllAsync(CancellationToken cancellationToken);
}
