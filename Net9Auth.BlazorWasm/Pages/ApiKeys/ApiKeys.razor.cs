using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Net9Auth.BlazorWasm.Services.ApiKeys;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;
using static System.Array;
using static Net9Auth.Shared.Infrastructure.Functional.Errors.ResultErrorFactory;
using static Net9Auth.Shared.Infrastructure.Functional.Result;


namespace Net9Auth.BlazorWasm.Pages.ApiKeys;

public partial class ApiKeys : ComponentBase
{
    protected readonly PaginationState? Pagination = new() { ItemsPerPage = 10 };

    protected GridItemsProvider<ApiKeyDto>? ApiKeysProvider;
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected IApiKeyService? ApiKeyService { get; set; }

    protected override Task OnInitializedAsync()
    {
        ApiKeysProvider = async req =>
        {
            var result = await GetApiKeysAsync();
            if (result.IsFailure) return GridItemsProviderResult.From(Empty<ApiKeyDto>(), 0);

            if (result.Value?.Items == null || result.Value.Items.Count == 0)
                return GridItemsProviderResult.From(Empty<ApiKeyDto>(), 0);

            StateHasChanged();

            return GridItemsProviderResult.From(
                result.Value.Items.Skip(req.StartIndex).Take(10).ToList(),
                result.Value.Items.Count);
        };

        return Task.CompletedTask;
    }

    private async Task<Result<PagedResultDto<ApiKeyDto>?>> GetApiKeysAsync()
    {
        if (ApiKeyService == null) return Fail<PagedResultDto<ApiKeyDto>?>(ApiServiceIsNull(""));
        var getListResult = await ApiKeyService.GetListAsync(new GetApiKeyListDto());
        return getListResult.IsSuccess ? getListResult : Fail<PagedResultDto<ApiKeyDto>?>(getListResult.Error);
    }

    private void GotoDetail(ApiKeyDto user) => NavigationManager?.NavigateTo($"admin/api-keys/detail/{user.Id}", true);

    private async Task CopyToClipboard(ApiKeyDto context)
    {
        await Task.CompletedTask;
    }
}