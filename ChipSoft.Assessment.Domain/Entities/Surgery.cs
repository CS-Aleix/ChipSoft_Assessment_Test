using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Domain.Entities;

internal class Surgery : Treatment
{
    public Surgery()
    {
        TreatmentType = TreatmentType.Surgery;
    }

    public override double CalculateCost()
    {
        return 1500d;
    }
}
