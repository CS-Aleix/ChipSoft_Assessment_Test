using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Domain.Entities;

public class Medication : Treatment
{
    public Medication()
    {
        TreatmentType = TreatmentType.Medication;
    }

    public override double CalculateCost()
    {
        return 200d;
    }
}
