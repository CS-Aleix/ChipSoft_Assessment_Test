using ChipSoft.Assessment.Domain.Enums;
using ChipSoft.Assessment.Domain.Interfaces;

namespace ChipSoft.Assessment.Domain.Entities;

public class Patient : Person, IPatient
{
    public string InsuranceNumber { get; set; } = string.Empty;
    public ICollection<Appointment> Appointments { get; set; } = [];
}