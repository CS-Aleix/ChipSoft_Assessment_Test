using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Application.DTOs;

public class DoctorOverviewDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
}
