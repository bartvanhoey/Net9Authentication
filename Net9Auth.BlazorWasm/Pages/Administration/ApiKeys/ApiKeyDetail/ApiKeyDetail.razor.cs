using Microsoft.AspNetCore.Components;
using Net9Auth.BlazorWasm.Services.Administration.ApiKeys;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.BlazorWasm.Pages.Administration.ApiKeys.ApiKeyDetail;

public partial class ApiKeyDetail : ComponentBase
{
    [Parameter] public string? Id { get; set; }
    [Inject] protected IApiKeyService? ApiKeyService { get; set; }

    private string? ErrorMessage { get; set; } = string.Empty;
    private ApiKeyDto? ApiKeyDto { get; set; }
    private bool ShowSpinner { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id == null) return;
        if (ApiKeyService == null) return;

        ShowSpinner = true;
        
        var result = await ApiKeyService.GetAsync(Guid.Parse(Id));
        if (result.IsFailure)
            ErrorMessage = $"Error: Could not load the requested API key";
        else
            ApiKeyDto = result.Value;

        ShowSpinner = false;
        StateHasChanged();
    }

}