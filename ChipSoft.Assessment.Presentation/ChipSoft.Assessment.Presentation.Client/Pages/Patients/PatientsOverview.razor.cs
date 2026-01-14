using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ChipSoft.Assessment.Application.DTOs;

namespace ChipSoft.Assessment.Presentation.Client.Pages.Patients;

public partial class PatientsOverview : ComponentBase
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected List<PatientOverviewDTO>? Patients { get; set; }
    protected List<string> Errors { get; set; } = new List<string>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var client = ClientFactory.CreateClient("Default");
            var response = await client.GetAsync("api/patients");
            if (response.IsSuccessStatusCode)
            {
                Patients = await response.Content.ReadFromJsonAsync<List<PatientOverviewDTO>>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Errors = await response.Content.ReadFromJsonAsync<List<string>>() ?? new List<string> { "Bad request" };
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                Errors.Add($"Server error ({(int)response.StatusCode}): {body}");
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex.Message);
        }
    }

    protected void NavigateToAdd()
    {
        Navigation.NavigateTo("/patients/add");
    }
}
