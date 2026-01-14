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
    //var nav = sp.GetRequiredService<NavigationManager>();
    //client.BaseAddress = new Uri(nav.BaseUri);
    var config = sp.GetRequiredService<IConfiguration>();
    var baseAddress = config.GetValue<string>("ApiSettings:BaseAddress") ?? throw new InvalidOperationException("BaseAddress configuration is missing.");

    client.BaseAddress = new Uri(baseAddress);
});

//builder.Services.AddScoped(sp =>
//{
//    var nav = sp.GetRequiredService<NavigationManager>();
//    return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
//});

// Register controllers so the API endpoint is available
builder.Services.AddControllers();

// existing registrations
builder.Services.AddInfrastructure();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

    // Checks if the database exists, if not, creates it based on the current model
    dbContext.Database.EnsureCreated();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

// Map API controllers
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChipSoft.Assessment.Presentation.Client._Imports).Assembly);

app.Run();
