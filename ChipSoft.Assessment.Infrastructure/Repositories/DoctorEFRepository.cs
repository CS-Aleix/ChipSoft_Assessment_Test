using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class DoctorEFRepository : GenericEFRepository<Doctor>, IDoctorRepository
{
    public DoctorEFRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
