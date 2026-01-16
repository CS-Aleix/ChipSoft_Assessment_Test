using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<Result<Patient>> AddAsync(Patient patient, CancellationToken cancellationToken);
    Task<Result<List<Patient>>> GetAllAsync(CancellationToken cancellationToken);
}
