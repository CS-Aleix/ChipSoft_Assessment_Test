using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class PatientEFRepository : GenericEFRepository<Patient>, IPatientRepository
{
    public PatientEFRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}