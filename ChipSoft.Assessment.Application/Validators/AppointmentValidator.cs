using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;
using System.Linq;

namespace ChipSoft.Assessment.Application.Validators;

public class AppointmentValidator
{
    internal static Result<Appointment> Validate(Appointment appointment, IEnumerable<Appointment> existingAppointments)
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

        if (!errors.Any())
        {
            var existing = existingAppointments ?? Enumerable.Empty<Appointment>();

            static bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
                => start1 < end2 && start2 < end1;

            var overlapping = existing.Where(a => a.Id != appointment.Id && Overlaps(appointment.StartTime, appointment.EndTime, a.StartTime, a.EndTime)).ToList();

            var doctorConflict = overlapping.Any(a => a.DoctorId == appointment.DoctorId);
            var patientConflict = overlapping.Any(a => a.PatientId == appointment.PatientId);

            if (doctorConflict)
            {
                errors.Add("Appointment overlaps with another appointment for the same doctor.");
            }

            if (patientConflict)
            {
                errors.Add("Appointment overlaps with another appointment for the same patient.");
            }
        }

        return new Result<Appointment>
        {
            IsSuccess = errors.Count == 0,
            Errors = errors
        };
    }
}
