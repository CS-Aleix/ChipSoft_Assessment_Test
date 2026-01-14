using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ChipSoft.Assessment.Application.DTOs;

namespace ChipSoft.Assessment.Presentation.Client.Pages.Doctors;

public partial class AddDoctor : ComponentBase
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected DoctorCreationDTO Model { get; set; } = new();
    protected List<string> Errors { get; set; } = new();

    protected async Task OnValidSubmit()
    {
        Errors.Clear();

        try
        {
            var client = ClientFactory.CreateClient("Default");
            var response = await client.PostAsJsonAsync("api/doctors", Model);

            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo("/doctors");
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
