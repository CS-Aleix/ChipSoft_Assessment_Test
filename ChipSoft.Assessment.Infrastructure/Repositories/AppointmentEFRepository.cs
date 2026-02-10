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

    Task<Result<Appointment>> IAppointmentRepository.AddAsync(Appointment appointment, CancellationToken cancellationToken) => throw new NotImplementedException();
    Task<Result<List<Appointment>>> IAppointmentRepository.GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
