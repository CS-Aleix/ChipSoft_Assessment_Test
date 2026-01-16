using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChipSoft.Assessment.Infrastructure.Repositories;

public class AppointmentEFRepository : IAppointmentRepository
{
    public AppointmentEFRepository(AppDbContext dbContext) 
    {
    }

    public async Task<Result<List<Appointment>>> GetOtherAppointmentsByDate(Appointment appointment, CancellationToken cancellationToken = default)
    {
        return new Result<List<Appointment>>();
    }

    Task<Result<Appointment>> IAppointmentRepository.AddAsync(Appointment appointment, CancellationToken cancellationToken) => throw new NotImplementedException();
    Task<Result<List<Appointment>>> IAppointmentRepository.GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
