using System.Linq;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.Validators;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Services;

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    public async Task<Result<Doctor>> AddDoctorAsync(DoctorCreationDTO doctorDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(doctorDto);

        var doctor = MapToDoctor(doctorDto);

        var result = DoctorValidator.ValidateDoctor(doctor);

        if (!result.IsSuccess)
        {
            return await Task.FromResult(result);
        }

        return await doctorRepository.AddAsync(doctor, cancellationToken);
    }

    public async Task<Result<IEnumerable<DoctorOverviewDTO>>> GetAllDoctorsAsync(CancellationToken cancellationToken = default)
    {
        var doctors = await doctorRepository.GetAllAsync(cancellationToken);
        return MapToDTOCollection(doctors);
    }

    private static Doctor MapToDoctor(DoctorCreationDTO doctorDto)
    {
        return new Doctor
        {
            Id = doctorDto.Id ?? 0,
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            DateOfBirth = doctorDto.DateOfBirth,
            Gender = doctorDto.Gender,
            Email = doctorDto.Email,
            LicenseNumber = doctorDto.LicenseNumber
        };
    }

    private static Result<IEnumerable<DoctorOverviewDTO>> MapToDTOCollection(Result<List<Doctor>> doctorsResult)
    {
        ArgumentNullException.ThrowIfNull(doctorsResult);

        if (!doctorsResult.IsSuccess)
        {
            return new Result<IEnumerable<DoctorOverviewDTO>>
            {
                IsSuccess = false,
                Errors = doctorsResult.Errors ?? new List<string>()
            };
        }

        var dtoList = (doctorsResult.Data ?? new List<Doctor>())
            .Select(d => new DoctorOverviewDTO
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                DateOfBirth = d.DateOfBirth,
                Gender = d.Gender,
                Email = d.Email,
                LicenseNumber = d.LicenseNumber
            })
            .ToList();

        return new Result<IEnumerable<DoctorOverviewDTO>>
        {
            IsSuccess = true,
            Data = dtoList
        };
    }
}
