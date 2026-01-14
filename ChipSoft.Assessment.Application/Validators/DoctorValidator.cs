using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Validators;

public class DoctorValidator
{
    internal static Result<Doctor> ValidateDoctor(Doctor doctor)
    {
        return new Result<Doctor>
        {
            IsSuccess = !string.IsNullOrWhiteSpace(doctor.LicenseNumber),
            Errors = !string.IsNullOrWhiteSpace(doctor.LicenseNumber)
                ? new List<string>()
                : new List<string> { "License number is required." }
        };
    }
}
