using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Application.DTOs;

public class PatientOverviewDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; } = Gender.Male;
}
