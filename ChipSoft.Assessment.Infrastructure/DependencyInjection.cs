using ChipSoft.Assessment.Application.Interfaces.Repositories;
using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChipSoft.Assessment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IDoctorRepository, DoctorEFRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceEFRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentEFRepository>();
        services.AddScoped<IPatientRepository, PatientEFRepository>();

        return services;
    }
}
