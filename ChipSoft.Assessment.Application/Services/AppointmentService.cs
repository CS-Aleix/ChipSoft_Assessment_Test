using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.Validators;
using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using System.Linq;

namespace ChipSoft.Assessment.Application.Services;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<Result<Appointment>> AddAppointmentAsync(AppointmentCreationDTO appointmentDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appointmentDto);

        Appointment appointment = MapToAppointment(appointmentDto);

        var overlappingAppointments = await appointmentRepository.GetOtherAppointmentsByDate(appointment, cancellationToken);
        var result = AppointmentValidator.Validate(appointment, overlappingAppointments.Data ?? []);

        if (!result.IsSuccess)
        {
            return result;
        }

        return await appointmentRepository.AddAsync(appointment, cancellationToken);
    }

    public async Task<Result<IEnumerable<AppointmentOverviewDTO>>> GetAllAppointmentsAsync(CancellationToken cancellationToken = default)
    {
        var result = await appointmentRepository.GetAllAsync(cancellationToken);
        return MapToDTOCollection(result);
    }

    private static Appointment MapToAppointment(AppointmentCreationDTO appointmentDto)
    {
        ArgumentNullException.ThrowIfNull(appointmentDto);
        ArgumentNullException.ThrowIfNull(appointmentDto.Patient);
        ArgumentNullException.ThrowIfNull(appointmentDto.Doctor);

        return new Appointment
        {
            StartTime = appointmentDto.StartTime,
            EndTime = appointmentDto.EndTime,
            Reason = appointmentDto.Reason ?? string.Empty,
            PatientId = appointmentDto.Patient?.Id ?? throw new ArgumentException("Patient Id required"),
            DoctorId = appointmentDto.Doctor?.Id ?? throw new ArgumentException("Doctor Id required")
        };
    }

    private static Result<IEnumerable<AppointmentOverviewDTO>> MapToDTOCollection(Result<List<Appointment>> appointmentResult)
    {
        ArgumentNullException.ThrowIfNull(appointmentResult);

        if (!appointmentResult.IsSuccess)
        {
            return new Result<IEnumerable<AppointmentOverviewDTO>>
            {
                IsSuccess = false,
                Errors = appointmentResult.Errors ?? new List<string>()
            };
        }

        var dtoList = (appointmentResult.Data ?? new List<Appointment>())
            .Select(a => new AppointmentOverviewDTO
            {
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                PatientName = $"{a.Patient?.FirstName} {a.Patient?.LastName}".Trim(),
                DoctorName = $"{a.Doctor?.FirstName} {a.Doctor?.LastName}".Trim(),
                Reason = a.Reason ?? string.Empty
            })
            .ToList();

        return new Result<IEnumerable<AppointmentOverviewDTO>>
        {
            IsSuccess = true,
            Data = dtoList
        };
    }
}
