using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Domain.Interfaces;

public interface IDoctor
{
    ICollection<Appointment> Appointments { get; set; }
}