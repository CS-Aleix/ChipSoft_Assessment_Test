using ChipSoft.Assessment.Domain.Entities;

namespace ChipSoft.Assessment.Domain.Interfaces
{
    internal interface IPatient
    {
        ICollection<Appointment> Appointments { get; set; }
    }
}