using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Application.Interfaces.Services;

public interface ITreatmentService
{
    List<TreatmentType> GetAllTreatmentTypes();
}
