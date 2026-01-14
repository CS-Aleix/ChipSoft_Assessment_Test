using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Application.Services;

public class TreatmentService : ITreatmentService
{
    public List<TreatmentType> GetAllTreatmentTypes()
        => Enum.GetValues(typeof(TreatmentType)).Cast<TreatmentType>().ToList();
}
