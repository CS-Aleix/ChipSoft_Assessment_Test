using ChipSoft.Assessment.Domain.Interfaces;

namespace ChipSoft.Assessment.Domain.Entities;

public class Appointment : BaseEntity, IAppointment
{
    public required DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public required int PatientId { get; set; }
    public required int DoctorId { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }

    public string Reason { get; set; }
    public List<Treatment>? Treatments { get; set; }
    public Invoice? Invoice { get; set; }
}