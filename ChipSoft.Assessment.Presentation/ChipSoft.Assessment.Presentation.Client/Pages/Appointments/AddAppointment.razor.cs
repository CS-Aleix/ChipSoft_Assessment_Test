using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ChipSoft.Assessment.Application.DTOs;
using ChipSoft.Assessment.Domain.Enums;

namespace ChipSoft.Assessment.Presentation.Client.Pages.Appointments;

public partial class AddAppointment : ComponentBase
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected AppointmentCreationDTO Model { get; set; } = new() { Patient = null, Doctor = null };
    protected List<string> Errors { get; set; } = new();

    protected DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    protected string StartTime { get; set; } = "09:00";

    protected DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    protected string EndTime { get; set; } = "09:30";

    protected List<TreatmentType> AvailableTreatmentTypes { get; set; } = new();

    protected List<PatientOverviewDTO>? Patients { get; set; }
    protected List<DoctorOverviewDTO>? Doctors { get; set; }

    protected int? SelectedPatientId { get; set; }
    protected int? SelectedDoctorId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var client = ClientFactory.CreateClient("Default");

            var response = await client.GetAsync("api/treatments");
            if (response.IsSuccessStatusCode)
            {
                AvailableTreatmentTypes = await response.Content.ReadFromJsonAsync<List<TreatmentType>>() ?? new();
            }
            //AvailableTreatmentTypes = await Http.GetFromJsonAsync<List<TreatmentType>>("api/treatments") ?? new();

            response = await client.GetAsync("api/patients");
            if (response.IsSuccessStatusCode)
            {
                Patients = await response.Content.ReadFromJsonAsync<List<PatientOverviewDTO>>() ?? new();
            }
            //Patients = await Http.GetFromJsonAsync<List<PatientOverviewDTO>>("api/patients") ?? new();

            response = await client.GetAsync("api/doctors");
            if (response.IsSuccessStatusCode)
            {
                Doctors = await response.Content.ReadFromJsonAsync<List<DoctorOverviewDTO>>() ?? new();
            }
            //Doctors = await Http.GetFromJsonAsync<List<DoctorOverviewDTO>>("api/doctors") ?? new();
        }
        catch (Exception ex)
        {
            Errors.Add(ex.Message);
        }
    }

    protected async Task OnValidSubmit()
    {
        Errors.Clear();

        try
        {
            if (SelectedPatientId is null || SelectedDoctorId is null)
            {
                Errors.Add("Please select both a patient and a doctor.");
                return;
            }

            // Combine date and time inputs into DateTime for the DTO
            Model.StartTime = DateTime.Parse($"{StartDate:yyyy-MM-dd} {StartTime}");
            Model.EndTime = DateTime.Parse($"{EndDate:yyyy-MM-dd} {EndTime}");

            Model.Patient = new PatientCreationDTO { Id = SelectedPatientId };
            Model.Doctor = new DoctorCreationDTO { Id = SelectedDoctorId };

            var client = ClientFactory.CreateClient("Default");
            var response = await client.PostAsJsonAsync("api/appointments", Model);

            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo("/appointments");
                return;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errors = await response.Content.ReadFromJsonAsync<List<string>>() ?? new List<string> { "Bad request" };
                Errors.AddRange(errors);
                return;
            }

            var body = await response.Content.ReadAsStringAsync();
            Errors.Add($"Server error ({(int)response.StatusCode}): {body}");
        }
        catch (Exception ex)
        {
            Errors.Add(ex.Message);
        }
    }
}
