using ChipSoft.Assessment.Domain.Interfaces;

namespace ChipSoft.Assessment.Domain.Entities;

public class Doctor : Person, IDoctor
{
    public required string LicenseNumber { get; set; } = string.Empty;
    public ICollection<Appointment> Appointments { get; set; } = [];

}
