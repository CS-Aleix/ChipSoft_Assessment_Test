using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class AppointmentEFRepository : GenericEFRepository<Appointment>, IAppointmentRepository
{
    public AppointmentEFRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Appointment> ApplyIncludes(IQueryable<Appointment> query)
    {
        return query.Include(a => a.Patient).Include(a => a.Doctor);
    }

    public async Task<Result<List<Appointment>>> GetOtherAppointmentsByDate(Appointment appointment, CancellationToken cancellationToken = default)
    {
        var result = new Result<List<Appointment>>();

        var list = await Query()
            .Where(a => a.Id != appointment.Id && a.StartTime.Date == appointment.StartTime.Date)
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        result.IsSuccess = true;
        result.Data = list;
        return result;
    }
}