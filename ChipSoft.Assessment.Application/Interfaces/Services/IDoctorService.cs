using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface IDoctorService
{
    Task<Result<Doctor>> AddDoctorAsync(DoctorCreationDTO doctorDto, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<DoctorOverviewDTO>>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);
}