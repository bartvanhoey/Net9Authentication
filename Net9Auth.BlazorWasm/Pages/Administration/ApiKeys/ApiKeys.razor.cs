using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Net9Auth.BlazorWasm.Services.Administration.ApiKeys;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;
using static System.Array;
using static Net9Auth.Shared.Infrastructure.Functional.Errors.ResultErrorFactory;
using static Net9Auth.Shared.Infrastructure.Functional.Result;


namespace Net9Auth.BlazorWasm.Pages.Administration.ApiKeys;

public partial class ApiKeys : ComponentBase
{
    private readonly PaginationState? _pagination = new() { ItemsPerPage = 10 };
    private GridItemsProvider<ApiKeyDto>? _apiKeysProvider;
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IApiKeyService? ApiKeyService { get; set; }

    private long _numResults = 0;
    protected bool ShowSpinner { get; set; }
    public string ApiKey { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;

    protected override Task OnInitializedAsync() => LoadGridItemsAsync();

    private Task LoadGridItemsAsync()
    {
        _apiKeysProvider = async req =>
        {
            var result = await GetApiKeysAsync();
            if (result.IsFailure) return GridItemsProviderResult.From(Empty<ApiKeyDto>(), 0);


            if (result.Value?.Items == null || result.Value.Items.Count == 0)
            {
                if (result.Value != null) _numResults = result.Value.TotalCount;
                return GridItemsProviderResult.From(Empty<ApiKeyDto>(), 0);
            }

            StateHasChanged();

            return GridItemsProviderResult.From(
                result.Value.Items.Skip(req.StartIndex).Take(10).ToList(),
                result.Value.Items.Count);
        };
        return Task.CompletedTask;
    }

    private async Task ApiKeyCreated_Handler(ApiKeyDto apiKeyDto)
    {
        ApiKey = apiKeyDto.Key;
        
        await LoadGridItemsAsync();
    }


    private async Task<Result<PagedResultDto<ApiKeyDto>?>> GetApiKeysAsync()
    {
        Result<PagedResultDto<ApiKeyDto>?> ret;
        if (ApiKeyService == null) ret = Fail<PagedResultDto<ApiKeyDto>?>(ApiServiceIsNull(""));
        else
        {
            var getListResult = await ApiKeyService.GetListAsync(new GetApiKeyListDto());
            ret = getListResult.IsSuccess ? getListResult : Fail<PagedResultDto<ApiKeyDto>?>(getListResult.Error);
        }

        return ret;
    }

    private void GotoDetail(ApiKeyDto user) => NavigationManager?.NavigateTo($"admin/api-keys/detail/{user.Id}", true);

    private async Task CopyToClipboard(ApiKeyDto context)
    {
        await Task.CompletedTask;
    }
}