using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class DoctorEFRepository : IDoctorRepository
{
    public DoctorEFRepository(AppDbContext dbContext)
    {
    }

    public Task<Result<Doctor>> AddAsync(Doctor doctor, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Result<List<Doctor>>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
