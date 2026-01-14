using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Domain.Entities;

public class Person : BaseEntity
{
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required DateOnly DateOfBirth { get; set; }
    public required Gender Gender { get; set; } = Gender.Male;
    public string Email { get; set; } = string.Empty;
}