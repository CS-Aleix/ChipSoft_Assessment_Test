using System.Linq;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.Validators;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Services;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<Result<Patient>> AddPatientAsync(PatientCreationDTO patientDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(patientDto);

        Patient patient = MapToPatient(patientDto);

        var result = PatientValidator.ValidatePatient(patient);

        if (!result.IsSuccess)
        {
            return await Task.FromResult(result);
        }

        return await patientRepository.AddAsync(patient, cancellationToken);
    }

    public async Task<Result<IEnumerable<PatientOverviewDTO>>> GetAllPatientsAsync(CancellationToken cancellationToken = default)
    {
        var patients = await patientRepository.GetAllAsync(cancellationToken);
        return MapToDTOCollection(patients);
    }

    private static Patient MapToPatient(PatientCreationDTO patientDto)
    {
        ArgumentNullException.ThrowIfNull(patientDto);

        return new Patient
        {
            Id = patientDto.Id ?? 0,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            DateOfBirth = patientDto.DateOfBirth,
            Gender = patientDto.Gender,
            Email = patientDto.Email,
            InsuranceNumber = patientDto.InsuranceNumber
        };
    }

    private static Result<IEnumerable<PatientOverviewDTO>> MapToDTOCollection(Result<List<Patient>> patientsResult)
    {
        ArgumentNullException.ThrowIfNull(patientsResult);

        if (!patientsResult.IsSuccess)
        {
            return new Result<IEnumerable<PatientOverviewDTO>>
            {
                IsSuccess = false,
                Errors = patientsResult.Errors ?? new List<string>()
            };
        }

        var dtoList = (patientsResult.Data ?? new List<Patient>())
            .Select(p => new PatientOverviewDTO
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender
            })
            .ToList();

        return new Result<IEnumerable<PatientOverviewDTO>>
        {
            IsSuccess = true,
            Data = dtoList
        };
    }
}