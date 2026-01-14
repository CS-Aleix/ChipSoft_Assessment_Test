using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface IPatientService
{
    Task<Result<Patient>> AddPatientAsync(PatientCreationDTO patient, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PatientOverviewDTO>>> GetAllPatientsAsync(CancellationToken cancellationToken = default);
}