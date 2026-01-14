using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Domain.Entities;
using ChipSoft.Assessment.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ChipSoft.Assessment.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext() : base()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    private bool isBusy;
    public async Task<(bool error, string message)> ResetAndReseedAsync()
    {
        bool error = false;
        if (isBusy)
        {
            error = true;
            return (error, "Busy, resetting at the moment.");
        }

        isBusy = true;
        string returnMessage = string.Empty;        

        try
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            returnMessage = "Database has been reset and reseeded.";
        }
        catch (Exception ex)
        {
            error = true;
            returnMessage = $"Failed to reset database: {ex.Message}";
        }
        finally
        {
            isBusy = false;
        }

        return (error, returnMessage ?? "Error while resetting database.");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured) options.UseSqlite($"Data Source=temp.db");
    }

    public DbSet<Patient>? Patients { get; set; }
    public DbSet<Appointment>? Appointments { get; set; }
    public DbSet<Doctor>? Doctors { get; set; }
    public DbSet<Invoice>? Invoices { get; set; }
    public DbSet<Treatment>? Treatments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(1980, 1, 1),
                Gender = Gender.Male,
                Email = "john.doe@example.com",
                InsuranceNumber = "INS12345"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateOnly(1990, 6, 15),
                Gender = Gender.Female,
                Email = "jane.smith@example.com",
                InsuranceNumber = "INS67890"
            }
        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FirstName = "Emily",
                LastName = "Taylor",
                DateOfBirth = new DateOnly(1975, 3, 20),
                Gender = Gender.Female,
                Email = "emily.taylor@clinic.example",
                LicenseNumber = "LIC1001"
            },
            new Doctor
            {
                Id = 2,
                FirstName = "Robert",
                LastName = "Brown",
                DateOfBirth = new DateOnly(1982, 11, 5),
                Gender = Gender.Male,
                Email = "robert.brown@clinic.example",
                LicenseNumber = "LIC1002"
            }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new
            {
                Id = 1,
                StartTime = new DateTime(2024, 1, 10, 9, 0, 0),
                EndTime = new DateTime(2024, 1, 10, 9, 30, 0),
                Reason = "Regular check-up",
                PatientId = 1,
                DoctorId = 1
            },
            new
            {
                Id = 2,
                StartTime = new DateTime(2024, 1, 11, 10, 0, 0),
                EndTime = new DateTime(2024, 1, 11, 10, 45, 0),
                Reason = "Follow-up",
                PatientId = 2,
                DoctorId = 2
            }
        );
    }
}
