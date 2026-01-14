using ChipSoft.Assessment.Application.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace ChipSoft.Assessment.Presentation.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] public IAppDbContext DbContext { get; set; } = default!;
    public string ReturnMessage { get; private set; }
    public bool HasError { get; private set; }

    private bool isBusy;

    private async Task ResetAndReseedAsync()
    {
        if (isBusy) return;
        isBusy = true;
        try
        {
            (HasError, ReturnMessage) = await DbContext.ResetAndReseedAsync();
        }
        finally
        {
            isBusy = false;
            StateHasChanged();
        }
    }
}
