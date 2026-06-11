using ChipSoft.Assessment.Application.Interfaces.Services;
using ChipSoft.Assessment.Application.Services;
using ChipSoft.Assessment.Presentation.Components;
using ChipSoft.Assessment.Infrastructure;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient("Default", (sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseAddress = config.GetValue<string>("ApiSettings:BaseAddress") ?? throw new InvalidOperationException("BaseAddress configuration is missing.");

    client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddControllers();

// Existing registrations
builder.Services.AddInfrastructure();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChipSoft.Assessment.Presentation.Client._Imports).Assembly);

app.Run();
