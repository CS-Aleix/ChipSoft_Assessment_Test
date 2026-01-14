namespace ChipSoft.Assessment.Application.DTOs;

public class AppointmentOverviewDTO
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
