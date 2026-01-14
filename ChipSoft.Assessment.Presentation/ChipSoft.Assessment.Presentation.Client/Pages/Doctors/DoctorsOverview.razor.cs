using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ChipSoft.Assessment.Application.DTOs;

namespace ChipSoft.Assessment.Presentation.Client.Pages.Doctors;

public partial class DoctorsOverview : ComponentBase
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected List<DoctorOverviewDTO>? Doctors { get; set; }
    protected List<string> Errors { get; set; } = new List<string>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var client = ClientFactory.CreateClient("Default");
            var response = await client.GetAsync("api/doctors");
            if (response.IsSuccessStatusCode)
            {
                Doctors = await response.Content.ReadFromJsonAsync<List<DoctorOverviewDTO>>();
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
        Navigation.NavigateTo("/doctors/add");
    }
}
