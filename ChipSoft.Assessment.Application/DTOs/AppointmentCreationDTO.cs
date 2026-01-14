using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Application.DTOs;

public class AppointmentCreationDTO
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Reason { get; set; }
    public PatientCreationDTO? Patient { get; set; }
    public DoctorCreationDTO? Doctor { get; set; }
    public TreatmentType? TreatmentType { get; set; }
}
