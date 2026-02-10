using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using System.Linq;

namespace ChipSoft.Assessment.Application.Validators;

public class AppointmentValidator
{
    internal static Result<Appointment> Validate(Appointment appointment)
    {
        if (appointment is null)
        {
            return new Result<Appointment>
            {
                IsSuccess = false,
                Errors = new List<string> { "Appointment cannot be null." }
            };
        }

        var errors = new List<string>();

        if (appointment.Patient is null && appointment.PatientId <= 0)
        {
            errors.Add("Patient is required.");
        }

        if (appointment.Doctor is null && appointment.DoctorId <= 0)
        {
            errors.Add("Doctor is required.");
        }

        if (appointment.StartTime == default)
        {
            errors.Add("StartTime is required.");
        }

        if (appointment.EndTime != default && appointment.EndTime < appointment.StartTime)
        {
            errors.Add("EndTime cannot be earlier than StartTime.");
        }

        return new Result<Appointment>
        {
            IsSuccess = errors.Count == 0,
            Errors = errors
        };
    }
}
