using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ChipSoft.Assessment.Application.DTOs;

namespace ChipSoft.Assessment.Presentation.Client.Pages.Patients;

public partial class AddPatient : ComponentBase
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected PatientCreationDTO Model { get; set; } = new();
    protected List<string> Errors { get; set; } = new();

    protected async Task OnValidSubmit()
    {
        Errors.Clear();

        try
        {
            var client = ClientFactory.CreateClient("Default");
            var response = await client.PostAsJsonAsync("api/patients", Model);

            if (response.IsSuccessStatusCode)
            {
                // Navigate to root or patient list after successful creation
                Navigation.NavigateTo("/");
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