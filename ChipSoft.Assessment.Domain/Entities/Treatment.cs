using ChipSoft.Assessment.Domain.Enums;
using ChipSoft.Assessment.Domain.Interfaces;

namespace ChipSoft.Assessment.Domain.Entities;

public class Treatment : BaseEntity, ITreatment
{
    public required Appointment Appointment { get; set; }

    public required TreatmentType TreatmentType { get; set; }

    public virtual double CalculateCost()
    {
        // Implementation of cost calculation
        return 0.0;
    }
}
