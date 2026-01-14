
using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Domain.Interfaces;

public interface IAppointment
{
    DateTime StartTime { get; set; }
    DateTime EndTime { get; set; }
    Patient Patient { get; set; }
    Doctor Doctor { get; set; }
    string Reason { get; set; }
    List<Treatment>? Treatments { get; set; }
    Invoice? Invoice { get; set; }
}