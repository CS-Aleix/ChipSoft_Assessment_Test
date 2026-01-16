using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class PatientEFRepository : IPatientRepository
{
    public PatientEFRepository(AppDbContext dbContext)
    {
    }

    public Task<Result<Patient>> AddAsync(Patient patient, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Result<List<Patient>>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
