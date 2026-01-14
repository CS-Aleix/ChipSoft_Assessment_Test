using ChipSoft.Assessment.Domain.Classes;
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Application.Validators;

public class PatientValidator
{
    internal static Result<Patient> ValidatePatient(Patient patient)
    {
        return new Result<Patient>
        {
            IsSuccess = !string.IsNullOrWhiteSpace(patient.InsuranceNumber),
            Errors = !string.IsNullOrWhiteSpace(patient.InsuranceNumber)
                ? new List<string>()
                : new List<string> { "Insurance number is required." }
        };
    }
}